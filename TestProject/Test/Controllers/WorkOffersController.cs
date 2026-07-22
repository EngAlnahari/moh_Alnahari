using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Microsoft.EntityFrameworkCore;

using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOffersController : ControllerBase
    {
        private readonly DbContextFirst _context;

        public WorkOffersController(DbContextFirst context)
        {
            _context = context;
        }

        // GET: api/WorkOffers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOffer>>> GetWorkOffer()
        {
            return await _context.WorkOffer.ToListAsync();
        }

        // GET: api/WorkOffers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkOffer>> GetWorkOffer(int id)
        {
            var workOffer = await _context.WorkOffer.FindAsync(id);

            if (workOffer == null)
            {
                return NotFound();
            }

            return workOffer;
        }

        // PUT: api/WorkOffers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkOffer(int id, WorkOffer workOffer)
        {
            if (id != workOffer.Id)
            {
                return BadRequest();
            }

            _context.Entry(workOffer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkOfferExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpGet("GetUserData/{offerId}")]
        public async Task<IActionResult> GetWorkOfferData(int offerId, string type, int space, string workType, string governorateParam, string transportationFeesPayer, string overNightFeesPayer, string allowanceFeesPayer, int? orderId)
        {
            workType = (workType ?? "").Trim();

            var workOfferEntity = await _context.WorkOffer
                .Include(w => w.transportationScheduleTemplate)
                .Include(w => w.workWagesTemplate)
                    .ThenInclude(wt => wt.workWagesTaples)
                .Include(w => w.additionalCostType)
                .Include(w => w.Users)
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == offerId);

            if (workOfferEntity == null)
                return NotFound("العرض غير موجود");

            var transportationSchedules = new List<object>();
            decimal totalTransportationFees = 0;
            decimal totalOverNightFees = 0;
            decimal totalAllowanceFees = 0;

            if (workOfferEntity.transportationScheduleTemplate != null)
            {
                var template = workOfferEntity.transportationScheduleTemplate;

                var transportModelsQuery = _context.TransportModels
                    .Where(t => t.TransportationScheduleTemplateID == template.Id);

                if (!string.IsNullOrEmpty(governorateParam))
                {
                    transportModelsQuery = transportModelsQuery
                        .Where(t => t.Governorate == governorateParam);
                }

                var transportModels = transportModelsQuery.ToList();

                // تجميع التكاليف حسب البراميترات لكل نوع تكلفة
                foreach (var m in transportModels)
                {
                    if ((transportationFeesPayer == "2" && m.StatuseType1 == "2") ||
                        (transportationFeesPayer == "3" && m.StatuseType2 == "3"))
                    {
                        totalTransportationFees += m.TransportationFees ?? 0;
                    }

                    if ((overNightFeesPayer == "2" && m.StatuseType1 == "2") ||
                        (overNightFeesPayer == "3" && m.StatuseType2 == "3"))
                    {
                        totalOverNightFees += m.OverNightFees ?? 0;
                    }

                    if ((allowanceFeesPayer == "2" && m.StatuseType1 == "2") ||
                        (allowanceFeesPayer == "3" && m.StatuseType2 == "3"))
                    {
                        totalAllowanceFees += m.AllowanceFees ?? 0;
                    }
                }

                transportationSchedules.Add(new
                {
                    TemplateId = template.Id,
                    TemplateName = template.NameTemplete,
                    StatuseType = template.StatuseType,
                    TransportModels = transportModels.Select(m => new
                    {
                        m.ID,
                        m.StatuseType1,
                        m.StatuseType2,
                        m.Governorate,
                        m.TransportationFees,
                        m.OverNightFees,
                        m.AllowanceFees,
                        IsSelected = true
                    }).ToList(),
                    TotalTransportationFees = totalTransportationFees,
                    TotalOverNightFees = totalOverNightFees,
                    TotalAllowanceFees = totalAllowanceFees,
                    TotalCost = totalTransportationFees + totalOverNightFees + totalAllowanceFees
                });
            }
            decimal totalCost = totalTransportationFees + totalOverNightFees + totalAllowanceFees;

            object workWagesTemplates = new List<object>();
            var specificSpaceTemplatesList = new List<object>();
            decimal selectedPrice = 0;
            decimal fromDate = 0;
            decimal toDate = 0;

            if (workType == "بالإنجاز (حسب المساحة)")
            {
                if (workOfferEntity.workWagesTemplate != null)
                {
                    var wagesTemplates = new[]
                    {
                new
                {
                    workOfferEntity.workWagesTemplate.Id,
                    workOfferEntity.workWagesTemplate.WorkWagesName,
                    workOfferEntity.workWagesTemplate.WorkWagesType,
                    workOfferEntity.workWagesTemplate.Notes,
                    WorkWagesTaples = workOfferEntity.workWagesTemplate.workWagesTaples
                        .Where(t => t.WorkType == workType && t.EachTypeSimilar == type)
                        .Select(t =>
                        {
                            decimal price = 0;
                            decimal datato = 0;
                            decimal datafrom = 0;
                            if (t.AlmjalID != null)
                            {
                                var specificTemplates = _context.specificSpaceTemplates
                                    .Where(s => s.AlmjalID == t.AlmjalID.Value)
                                    .ToList();

                                foreach (var s in specificTemplates)
                                {
                                    if ((s.LessThan.HasValue && space <= s.LessThan.Value) ||
                                        (s.BiggerThan.HasValue && space >= s.BiggerThan.Value) ||
                                        (s.SpaceFrom.HasValue && s.SpaceTo.HasValue && space >= s.SpaceFrom.Value && space <= s.SpaceTo.Value))
                                    {
                                        price = Convert.ToDecimal(s.Price ?? 0);
                                        datato = Convert.ToDecimal(s.ToDate ?? 0);
                                        datafrom = Convert.ToDecimal(s.FromDate );
                                        specificSpaceTemplatesList.Add(new
                                        {
                                            s.Id,
                                            s.Space,
                                            s.LessThan,
                                            s.BiggerThan,
                                            s.SpaceFrom,
                                            s.SpaceTo,
                                            s.Price,
                                          s.FromDate,
                                        s.ToDate,
                                            s.PriceWithDevice,
                                            s.Notes,
                                            AlmjalID = t.AlmjalID
                                        });
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                price = Convert.ToDecimal(t.Price ?? 0);
                                 datato = Convert.ToDecimal(t.ToDate ?? 0);
                                        datafrom = Convert.ToDecimal(t.FromDate ?? 0);
                            }

                            return new
                            {
                                t.Id,
                                t.EachTypeSimilar,
                                t.WorkType,
                                Price = price,
                                t.Price1,
                                t.SpaceStandard,
                                Datafrom =datafrom,
                                Datato =datato,
                                t.AlmjalID,
                              t.ToDate,
                                t.Notes
                            };
                        }).ToList()
                }
            };
                    workWagesTemplates = wagesTemplates;
                    selectedPrice = wagesTemplates.SelectMany(w => w.WorkWagesTaples).FirstOrDefault()?.Price ?? 0;
                    toDate = wagesTemplates.SelectMany(w => w.WorkWagesTaples).FirstOrDefault()?.ToDate ?? 0;
                    fromDate = wagesTemplates.SelectMany(w => w.WorkWagesTaples).FirstOrDefault()?.Datafrom ?? 0;
                }
            }
            else if (!string.IsNullOrEmpty(workType))
            {
                if (workOfferEntity.workWagesTemplate != null)
                {
                    var wagesTemplates = new[]
                    {
                new
                {
                    workOfferEntity.workWagesTemplate.Id,
                    workOfferEntity.workWagesTemplate.WorkWagesName,
                    workOfferEntity.workWagesTemplate.WorkWagesType,
                    workOfferEntity.workWagesTemplate.Notes,
                    WorkWagesTaples = workOfferEntity.workWagesTemplate.workWagesTaples
                        .Where(t => t.WorkType == workType && t.EachTypeSimilar == type)
                        .Select(t => new
                        {
                            t.Id,
                            t.EachTypeSimilar,
                            t.WorkType,
                            t.Price,
                            t.Price1,
                            t.SpaceStandard,
                            t.FromDate,
                            t.AlmjalID,
                            t.ToDate,
                            t.Notes
                        }).ToList()
                }
            };
                    workWagesTemplates = wagesTemplates;
                    selectedPrice = wagesTemplates.SelectMany(w => w.WorkWagesTaples).FirstOrDefault()?.Price ?? 0;
                    toDate = wagesTemplates.SelectMany(w => w.WorkWagesTaples).FirstOrDefault()?.ToDate ?? 0;
                    fromDate = wagesTemplates.SelectMany(w => w.WorkWagesTaples).FirstOrDefault()?.FromDate ?? 0;
                }
            }

            // التكاليف الإضافية
            var additionalCosts = new List<object>();
            decimal additionalCostsTotal = 0;

            if (workOfferEntity.additionalCostType != null)
            {
                var costType = workOfferEntity.additionalCostType;

                var costModels = _context.AdditionalCostModels
                    .Where(c => c.AdditionalCostTypeId == costType.Id)
                    .ToList();

                foreach (var cost in costModels)
                {
                    additionalCosts.Add(new
                    {
                        cost.ID,
                        CostTypeName = costType.AdditionalCostTypeName,
                        cost.Price,
                        IsSelected = true
                    });
                    additionalCostsTotal += cost.Price ?? 0;
                }
            }

            // تعديل هنا لتحميل كل الحقول في TanjezOrder
            var tanjezOrderDto = await _context.TanjezOrder
     .AsNoTracking()
     .Include(o => o.earthBorders)
     .Where(o => o.Id == orderId.Value)
     .Select(o => new TanjezOrderDto
     {
         Id = o.Id,
         CreateOrderName = o.CreateOrderName,
         PlaceName = o.PlaceName,
         Space = o.Space,
         TypePiece = o.TypePiece,
         City = o.City,

         UserID = o.UserID,
         Alhiaza = o.Alhiaza,
         EarthBorders = o.earthBorders.Select(e => new EarthBorderDto
         {
             ID = e.ID,
             BorderType = e.BorderType,
             BorderDescription = e.BorderDescription,
             IdenticalOrDifferent = e.IdenticalOrDifferent,
         }).ToList()
     })
     .FirstOrDefaultAsync();



            var result = new
            {
                workOfferEntity.Id,
                OfferName = workOfferEntity.OfferName,

                userid = workOfferEntity.UserID,
                workofferid = workOfferEntity.Id,
                OfferName1 = workOfferEntity.LatePenaltyPercentage,
                OfferName2 = workOfferEntity.AdvancePaymentPercentage,
                OfferName3 = workOfferEntity.ResignationAfterAdvancePercentage,
                OfferName4 = workOfferEntity.OnSitePaymentPercentage,
                OfferName5 = workOfferEntity.CamelResignationPenaltyPercentage,
                OfferName6 = workOfferEntity.AfterDeliveryPaymentPercentage,
                TransportationSchedules = transportationSchedules,
                WorkWagesTemplates = workWagesTemplates,
                SpecificSpaceTemplates = specificSpaceTemplatesList,
                AdditionalCosts = additionalCosts,
                AdditionalCostsTotal = additionalCostsTotal,
                SelectedPrice = selectedPrice,
                ToDate = toDate,
                FromDate = fromDate,
                TotalTransportationCost = totalCost,
                UserName = workOfferEntity.Users != null
                    ? $"{workOfferEntity.Users.Frist_Name} {workOfferEntity.Users.Second_Name} {workOfferEntity.Users.Third_Name} {workOfferEntity.Users.Fourth_Name}"
                    : null,
                TanjezOrder = tanjezOrderDto
            };

            return Ok(result);
        }

        //public async Task<IActionResult> GetUserData(int userId, string type, int space, string workType)
        //{
        //    workType = (workType ?? "").Trim();

        //    var userEntity = await _context.Users
        //        .Include(u => u.transportModels)
        //            .ThenInclude(m => m.transportationScheduleTemplate)
        //        .Include(u => u.workWagesTemplates)
        //            .ThenInclude(w => w.workWagesTaples)
        //        .Include(u => u.additionalCostModels)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(u => u.ID == userId);

        //    if (userEntity == null)
        //        return NotFound("المستخدم غير موجود");

        //    var transportationSchedules = userEntity.transportModels
        //        .GroupBy(m => m.TransportationScheduleTemplateID)
        //        .Select(g => new
        //        {
        //            TemplateId = g.Key,
        //            TemplateName = g.FirstOrDefault()?.transportationScheduleTemplate?.NameTemplete,
        //            StatuseType = g.FirstOrDefault()?.transportationScheduleTemplate?.StatuseType,
        //            TransportModels = g.Select(m => new
        //            {
        //                m.ID,
        //                m.StatuseType1,
        //                m.StatuseType2,
        //                m.Governorate,
        //                m.TransportationFees,
        //                m.OverNightFees,
        //                m.AllowanceFees,
        //                m.IsSelected
        //            }).ToList()
        //        }).ToList();

        //    object workWagesTemplates = new List<object>();
        //    var specificSpaceTemplatesList = new List<object>();
        //    decimal selectedPrice = 0;

        //    if (workType == "بالإنجاز (حسب المساحة)")
        //    {
        //        var wagesTemplates = userEntity.workWagesTemplates
        //            .Select(w => new
        //            {
        //                w.Id,
        //                w.WorkWagesName,
        //                w.WorkWagesType,
        //                w.Notes,
        //                WorkWagesTaples = w.workWagesTaples
        //                    .Where(t => t.WorkType == workType && t.EachTypeSimilar == type)
        //                    .Select(t =>
        //                    {
        //                        decimal price = 0;

        //                        if (t.AlmjalID != null)
        //                        {
        //                            // جلب كل SpecificSpaceTemplate المرتبطة بـ AlmjalID
        //                            var specificTemplates = _context.specificSpaceTemplates
        //                                .Where(s => s.AlmjalID == t.AlmjalID.Value)
        //                                .ToList();

        //                            foreach (var s in specificTemplates)
        //                            {
        //                                if ((s.LessThan.HasValue && space <= s.LessThan.Value) ||
        //                                    (s.BiggerThan.HasValue && space >= s.BiggerThan.Value) ||
        //                                    (s.SpaceFrom.HasValue && s.SpaceTo.HasValue && space >= s.SpaceFrom.Value && space <= s.SpaceTo.Value))
        //                                {
        //                                    price = Convert.ToDecimal(s.Price ?? 0);

        //                                    specificSpaceTemplatesList.Add(new
        //                                    {
        //                                        s.Id,
        //                                        s.Space,
        //                                        s.LessThan,
        //                                        s.BiggerThan,
        //                                        s.SpaceFrom,
        //                                        s.SpaceTo,
        //                                        s.Price,
        //                                        s.FromDate,
        //                                        s.ToDate,
        //                                        s.PriceWithDevice,
        //                                        s.Notes,
        //                                        AlmjalID = t.AlmjalID
        //                                    });

        //                                    // إذا وجدنا تطابق، يمكن الخروج من الحلقة
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            price = Convert.ToDecimal(t.Price ?? 0);
        //                        }

        //                        return new
        //                        {
        //                            t.Id,
        //                            t.EachTypeSimilar,
        //                            t.WorkType,
        //                            Price = price,
        //                            t.Price1,
        //                            t.SpaceStandard,
        //                            t.FromDate,
        //                            t.AlmjalID,
        //                            t.ToDate,
        //                            t.Notes
        //                        };
        //                    }).ToList()
        //            })
        //            .Where(w => w.WorkWagesTaples.Any())
        //            .ToList();

        //        workWagesTemplates = wagesTemplates;
        //        selectedPrice = wagesTemplates
        //            .SelectMany(w => w.WorkWagesTaples)
        //            .FirstOrDefault()?.Price ?? 0;
        //    }

        //    else if (!string.IsNullOrEmpty(workType))
        //    {
        //        var wagesTemplates = userEntity.workWagesTemplates
        //            .Select(w => new
        //            {
        //                w.Id,
        //                w.WorkWagesName,
        //                w.WorkWagesType,
        //                w.Notes,
        //                WorkWagesTaples = w.workWagesTaples
        //                    .Where(t => t.WorkType == workType && t.EachTypeSimilar == type)
        //                    .Select(t => new
        //                    {
        //                        t.Id,
        //                        t.EachTypeSimilar,
        //                        t.WorkType,
        //                        t.Price,
        //                        t.Price1,
        //                        t.SpaceStandard,
        //                        t.FromDate,
        //                        t.AlmjalID,
        //                        t.ToDate,
        //                        t.Notes
        //                    }).ToList()
        //            })
        //            .Where(w => w.WorkWagesTaples.Any())
        //            .ToList();

        //        workWagesTemplates = wagesTemplates;
        //        selectedPrice = wagesTemplates
        //            .SelectMany(w => w.WorkWagesTaples)
        //            .FirstOrDefault()?.Price ?? 0;
        //    }

        //    var additionalCosts = userEntity.additionalCostModels
        //        .Select(c => new { c.ID, c.CostType, c.Price, c.IsSelected })
        //        .ToList();

        //    var additionalCostsTotal = userEntity.additionalCostModels.Sum(c => (decimal?)c.Price) ?? 0;

        //    var result = new
        //    {
        //        userEntity.ID,
        //        UserName = $"{userEntity.Frist_Name} {userEntity.Second_Name} {userEntity.Third_Name} {userEntity.Fourth_Name}",
        //        userEntity.Nickname,
        //        userEntity.Phone_number1,
        //        userEntity.Email,
        //        TransportationSchedules = transportationSchedules,
        //        WorkWagesTemplates = workWagesTemplates,
        //        SpecificSpaceTemplates = specificSpaceTemplatesList,
        //        AdditionalCosts = additionalCosts,
        //        AdditionalCostsTotal = additionalCostsTotal,
        //        SelectedPrice = selectedPrice
        //    };

        //    return Ok(result);
        //}








        //public async Task<IActionResult> GetUserData(int userId, string type, int space, string workType)
        //{
        //    // تنظيف القيمة المرسلة
        //    workType = (workType ?? "").Trim();

        //    // جلب المستخدم مع العلاقات اللازمة
        //    var userEntity = await _context.Users
        //        .Include(u => u.transportModels)
        //            .ThenInclude(m => m.transportationScheduleTemplate)
        //        .Include(u => u.workWagesTemplates)
        //            .ThenInclude(w => w.workWagesTaples)
        //        .Include(u => u.additionalCostModels)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(u => u.ID == userId);

        //    if (userEntity == null)
        //        return NotFound("المستخدم غير موجود");

        //    // بناء بيانات التنقلات/المبيت
        //    var transportationSchedules = userEntity.transportModels
        //        .GroupBy(m => m.TransportationScheduleTemplateID)
        //        .Select(g => new
        //        {
        //            TemplateId = g.Key,
        //            TemplateName = g.FirstOrDefault()?.transportationScheduleTemplate?.NameTemplete,
        //            StatuseType = g.FirstOrDefault()?.transportationScheduleTemplate?.StatuseType,
        //            TransportModels = g.Select(m => new
        //            {
        //                m.ID,
        //                m.TypeStatus,
        //                m.Governorate,
        //                m.TransportationFees,
        //                m.OverNightFees,
        //                m.AllowanceFees,
        //                m.IsSelected
        //            }).ToList()
        //        }).ToList();

        //    // تهيئة القوائم
        //    object workWagesTemplates = new List<object>();
        //    var specificSpaceTemplatesList = new List<object>(); // قائمة SpecificSpaceTemplates النهائية
        //    decimal selectedPrice = 0; // السعر النهائي بعد التصنيف

        //    if (workType == "بالإنجاز (حسب المساحة)")
        //    {
        //        // جلب جميع Almjals مسبقًا كقائمة
        //        var almjalsList = await _context.Almjals
        //            .Include(a => a.specificSpaceTemplate)
        //            .ToListAsync();

        //        var wagesTemplates = userEntity.workWagesTemplates
        //            .Select(w => new
        //            {
        //                w.Id,
        //                w.WorkWagesName,
        //                w.WorkWagesType,
        //                w.Notes,
        //                WorkWagesTaples = w.workWagesTaples
        //                    .Where(t => t.WorkType == workType && t.EachTypeSimilar == type)
        //                    .Select(t =>
        //                    {
        //                        decimal price = 0;

        //                        if (t.AlmjalID != null)
        //                        {
        //                            var almjal = almjalsList.FirstOrDefault(a => a.ID == t.AlmjalID.Value);
        //                            if (almjal != null && almjal.specificSpaceTemplate != null)
        //                            {
        //                                var s = almjal.specificSpaceTemplate;
        //                                if ((s.LessThan.HasValue && space <= s.LessThan.Value) ||
        //                                    (s.BiggerThan.HasValue && space >= s.BiggerThan.Value) ||
        //                                    (s.SpaceFrom.HasValue && s.SpaceTo.HasValue && space >= s.SpaceFrom.Value && space <= s.SpaceTo.Value))
        //                                {
        //                                    price = Convert.ToDecimal(s.Price ?? 0);

        //                                    // إضافة السجل إلى SpecificSpaceTemplates
        //                                    specificSpaceTemplatesList.Add(new
        //                                    {
        //                                        s.Id,
        //                                        s.Space,
        //                                        s.LessThan,
        //                                        s.BiggerThan,
        //                                        s.SpaceFrom,
        //                                        s.SpaceTo,
        //                                        s.Price,
        //                                        s.FromDate,
        //                                        s.ToDate,
        //                                        s.PriceWithDevice,
        //                                        s.Notes,
        //                                        AlmjalID = t.AlmjalID
        //                                    });
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            price = Convert.ToDecimal(t.Price ?? 0);
        //                        }

        //                        return new
        //                        {
        //                            t.Id,
        //                            t.EachTypeSimilar,
        //                            t.WorkType,
        //                            Price = price,
        //                            t.Price1,
        //                            t.SpaceStandard,
        //                            t.FromDate,
        //                            t.AlmjalID,
        //                            t.ToDate,
        //                            t.Notes
        //                        };
        //                    }).ToList()
        //            })
        //            .Where(w => w.WorkWagesTaples.Any())
        //            .ToList();

        //        workWagesTemplates = wagesTemplates;
        //        selectedPrice = wagesTemplates
        //            .SelectMany(w => w.WorkWagesTaples)
        //            .FirstOrDefault()?.Price ?? 0;
        //    }
        //    else if (!string.IsNullOrEmpty(workType))
        //    {
        //        var wagesTemplates = userEntity.workWagesTemplates
        //            .Select(w => new
        //            {
        //                w.Id,
        //                w.WorkWagesName,
        //                w.WorkWagesType,
        //                w.Notes,
        //                WorkWagesTaples = w.workWagesTaples
        //                    .Where(t => t.WorkType == workType && t.EachTypeSimilar == type)
        //                    .Select(t => new
        //                    {
        //                        t.Id,
        //                        t.EachTypeSimilar,
        //                        t.WorkType,
        //                        t.Price,
        //                        t.Price1,
        //                        t.SpaceStandard,
        //                        t.FromDate,
        //                        t.AlmjalID,
        //                        t.ToDate,
        //                        t.Notes
        //                    }).ToList()
        //            })
        //            .Where(w => w.WorkWagesTaples.Any())
        //            .ToList();

        //        workWagesTemplates = wagesTemplates;
        //        selectedPrice = wagesTemplates
        //            .SelectMany(w => w.WorkWagesTaples)
        //            .FirstOrDefault()?.Price ?? 0;
        //    }

        //    // التكاليف الإضافية
        //    var additionalCosts = userEntity.additionalCostModels
        //        .Select(c => new { c.ID, c.CostType, c.Price, c.IsSelected })
        //        .ToList();

        //    var additionalCostsTotal = userEntity.additionalCostModels.Sum(c => (decimal?)c.Price) ?? 0;

        //    // بناء النتيجة النهائية
        //    var result = new
        //    {
        //        userEntity.ID,
        //        UserName = $"{userEntity.Frist_Name} {userEntity.Second_Name} {userEntity.Third_Name} {userEntity.Fourth_Name}",
        //        userEntity.Nickname,
        //        userEntity.Phone_number1,
        //        userEntity.Email,
        //        TransportationSchedules = transportationSchedules,
        //        WorkWagesTemplates = workWagesTemplates,
        //        SpecificSpaceTemplates = specificSpaceTemplatesList,
        //        AdditionalCosts = additionalCosts,
        //        AdditionalCostsTotal = additionalCostsTotal,
        //        SelectedPrice = selectedPrice
        //    };

        //    return Ok(result);
        //}



        //public async Task<IActionResult> GetUserData(int userId, string type, int space, string workType)
        //{
        //    // تنظيف القيمة المرسلة
        //    workType = (workType ?? "").Trim();

        //    // جلب المستخدم مع العلاقات اللازمة
        //    var userEntity = await _context.Users
        //        .Include(u => u.transportModels)
        //            .ThenInclude(m => m.transportationScheduleTemplate)
        //        .Include(u => u.workWagesTemplates)
        //            .ThenInclude(w => w.workWagesTaples)
        //        .Include(u => u.additionalCostModels)
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(u => u.ID == userId);

        //    if (userEntity == null)
        //        return NotFound("المستخدم غير موجود");

        //    // بناء بيانات التنقلات/المبيت
        //    var transportationSchedules = userEntity.transportModels
        //        .GroupBy(m => m.TransportationScheduleTemplateID)
        //        .Select(g => new
        //        {
        //            TemplateId = g.Key,
        //            TemplateName = g.FirstOrDefault()?.transportationScheduleTemplate?.NameTemplete,
        //            StatuseType = g.FirstOrDefault()?.transportationScheduleTemplate?.StatuseType,
        //            TransportModels = g.Select(m => new
        //            {
        //                m.ID,
        //                m.TypeStatus,
        //                m.Governorate,
        //                m.TransportationFees,
        //                m.OverNightFees,
        //                m.AllowanceFees,
        //                m.IsSelected
        //            }).ToList()
        //        }).ToList();

        //    // تهيئة القوائم
        //    object workWagesTemplates = new List<object>();
        //    object specificSpaceTemplates = new List<object>();
        //    decimal selectedPrice = 0; // السعر النهائي بعد التصنيف

        //    // الحالة: بالإنجاز (حسب المساحة)
        //    if (workType == "بالإنجاز (حسب المساحة)")
        //    {
        //        var spaceTemplates = await _context.specificSpaceTemplates
        //            .Where(s => s.UserID == userId &&
        //                (
        //                    (s.LessThan.HasValue && space <= s.LessThan) ||
        //                    (s.BiggerThan.HasValue && space >= s.BiggerThan) ||
        //                    (s.SpaceFrom.HasValue && s.SpaceTo.HasValue && space >= s.SpaceFrom && space <= s.SpaceTo)
        //                ))
        //            .OrderBy(s => s.Price)
        //            .Take(1)
        //            .Select(s => new
        //            {
        //                s.Id,
        //                s.LessThan,
        //                s.BiggerThan,
        //                s.SpaceFrom,
        //                s.SpaceTo,
        //                s.Space,
        //                s.Price,
        //                s.FromDate,
        //                s.ToDate,
        //                s.PriceWithDevice,
        //                s.Notes
        //            })
        //            .ToListAsync();

        //        specificSpaceTemplates = spaceTemplates;
        //        selectedPrice = spaceTemplates.FirstOrDefault()?.Price ?? 0;
        //    }
        //    // الحالة: أي قيمة أخرى -> البحث في WorkWagesTaples حسب t.WorkType و t.EachTypeSimilar
        //    else if (!string.IsNullOrEmpty(workType))
        //    {
        //        var wagesTemplates = userEntity.workWagesTemplates
        //            .Select(w => new
        //            {
        //                w.Id,
        //                w.WorkWagesName,
        //                w.WorkWagesType,
        //                w.Notes,
        //                WorkWagesTaples = w.workWagesTaples
        //                    .Where(t => t.WorkType == workType && t.EachTypeSimilar == type)
        //                    .Select(t => new
        //                    {
        //                        t.Id,
        //                        t.EachTypeSimilar,
        //                        t.WorkType,
        //                        t.Price,
        //                        t.Price1,
        //                        t.SpaceStandard,
        //                        t.FromDate,
        //                        t.AlmjalID,
        //                        t.ToDate,
        //                        t.Notes
        //                    }).ToList()
        //            })
        //            // نحذف أي قالب لم يتبق فيه أي WorkWagesTaples بعد الفلترة
        //            .Where(w => w.WorkWagesTaples.Any())
        //            .ToList();

        //        workWagesTemplates = wagesTemplates;
        //        selectedPrice = wagesTemplates
        //            .SelectMany(w => w.WorkWagesTaples)
        //            .FirstOrDefault()?.Price ?? 0;
        //    }

        //    // التكاليف الإضافية
        //    var additionalCosts = userEntity.additionalCostModels
        //        .Select(c => new { c.ID, c.CostType, c.Price, c.IsSelected })
        //        .ToList();

        //    var additionalCostsTotal = userEntity.additionalCostModels.Sum(c => (decimal?)c.Price) ?? 0;

        //    // بناء النتيجة النهائية
        //    var result = new
        //    {
        //        userEntity.ID,
        //        UserName = $"{userEntity.Frist_Name} {userEntity.Second_Name} {userEntity.Third_Name} {userEntity.Fourth_Name}",
        //        userEntity.Nickname,
        //        userEntity.Phone_number1,
        //        userEntity.Email,
        //        TransportationSchedules = transportationSchedules,
        //        WorkWagesTemplates = workWagesTemplates,
        //        SpecificSpaceTemplates = specificSpaceTemplates,
        //        AdditionalCosts = additionalCosts,
        //        AdditionalCostsTotal = additionalCostsTotal,
        //        SelectedPrice = selectedPrice // ✅ السعر النهائي بعد التصنيف
        //    };

        //    return Ok(result);
        //}









        [HttpGet("GetTransportModels/{userId}")]
        public async Task<IActionResult> GetTransportModels(int userId, string governorate, string statusType)
        {
            // جلب الصفوف من TransportModel المرتبطة بالمستخدم والنوع المطلوب
            var models = await _context.TransportModels
                .Where(m => m.transportationScheduleTemplate != null
                            && m.transportationScheduleTemplate.UserID == userId
                            && m.transportationScheduleTemplate.StatuseType == statusType
                            && m.Governorate == governorate)
                .Select(m => new
                {
                    m.ID,
                    m.StatuseType1,
                    m.StatuseType2,
                    m.Governorate,
                    m.TransportationFees,
                    m.OverNightFees,
                    m.AllowanceFees,
                    m.IsSelected,
                    TemplateName = m.transportationScheduleTemplate.NameTemplete,
                    TemplateStatus = m.transportationScheduleTemplate.StatuseType
                })
                .ToListAsync();

            if (!models.Any())
                return NotFound("لا توجد بيانات للمستخدم المحدد والمحافظة والنوع.");

            return Ok(models);
        }


        // POST: api/WorkOffers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorkOffer>> PostWorkOffer(WorkOffer workOffer)
        {
            _context.WorkOffer.Add(workOffer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkOffer", new { id = workOffer.Id }, workOffer);
        }

        // DELETE: api/WorkOffers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkOffer(int id)
        {
            var workOffer = await _context.WorkOffer.FindAsync(id);
            if (workOffer == null)
            {
                return NotFound();
            }

            _context.WorkOffer.Remove(workOffer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkOfferExists(int id)
        {
            return _context.WorkOffer.Any(e => e.Id == id);
        }
    }
}
