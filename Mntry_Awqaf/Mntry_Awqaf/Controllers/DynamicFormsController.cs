using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mntry_Awqaf.Data;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Controllers
{
    public class DynamicFormsController : Controller
    {
        private readonly DynamicFormsDbContext _context;

        public DynamicFormsController(DynamicFormsDbContext context)
        {
            _context = context;
        }

        // 1. Create a new field
        [HttpGet]
        public IActionResult CreateField()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateField(DynamicField model)
        {
            if (ModelState.IsValid)
            {
                _context.DynamicFields.Add(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "تم إنشاء الحقل بنجاح";
                return RedirectToAction(nameof(CreateField));
            }
            return View(model);
        }

        // 2. Assign Fields to Category and Screen
        [HttpGet]
        public async Task<IActionResult> AssignFields()
        {
            ViewBag.Fields = await _context.DynamicFields.ToListAsync();
            ViewBag.Assignments = await _context.FieldAssignments
                .Include(a => a.DynamicField)
                .GroupBy(a => new { a.GroupName, a.ScreenType, a.MainCategory, a.Detail })
                .ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AssignFields(string groupName, string main, string spec, string branch, string detail, string screenType, List<int> fieldIds)
        {
            if(string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(screenType))
            {
                TempData["ErrorMessage"] = "يجب إدخال اسم المجموعة واختيار الشاشة.";
                return RedirectToAction(nameof(AssignFields));
            }

            // Remove old assignments for this specific combination
            var existing = _context.FieldAssignments.Where(a => 
                a.GroupName == groupName && 
                a.ScreenType == screenType &&
                (string.IsNullOrEmpty(detail) || a.Detail == detail)
            );
            _context.FieldAssignments.RemoveRange(existing);

            // Add new assignments
            foreach (var id in fieldIds)
            {
                _context.FieldAssignments.Add(new FieldAssignment
                {
                    GroupName = groupName,
                    MainCategory = main ?? "",
                    Specialty = spec ?? "",
                    Branch = branch ?? "",
                    Detail = detail ?? "",
                    ScreenType = screenType,
                    DynamicFieldId = id
                });
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "تم تعيين الحقول للمجموعة بنجاح";
            return RedirectToAction(nameof(AssignFields));
        }


        [HttpPost]
        public async Task<IActionResult> EditField(int id, string name, string type, string options, bool isRequired)
        {
            var field = await _context.DynamicFields.FindAsync(id);
            if (field != null)
            {
                field.Name = name;
                field.Type = type;
                field.Options = options;
                field.IsRequired = isRequired;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "تم تعديل الحقل بنجاح.";
            }
            else
            {
                TempData["ErrorMessage"] = "لم يتم العثور على الحقل.";
            }
            return RedirectToAction(nameof(AssignFields));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteField(int id)
        {
            var field = await _context.DynamicFields.FindAsync(id);
            if (field != null)
            {
                var assignments = _context.FieldAssignments.Where(a => a.DynamicFieldId == id);
                _context.FieldAssignments.RemoveRange(assignments);
                
                _context.DynamicFields.Remove(field);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "تم حذف الحقل وتعييناته بنجاح.";
            }
            else
            {
                TempData["ErrorMessage"] = "لم يتم العثور على الحقل.";
            }
            return RedirectToAction(nameof(AssignFields));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var assignment = await _context.FieldAssignments.FindAsync(id);
            if (assignment != null)
            {
                _context.FieldAssignments.Remove(assignment);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "تم إزالة التعيين بنجاح.";
            }
            else
            {
                TempData["ErrorMessage"] = "لم يتم العثور على التعيين.";
            }
            return RedirectToAction(nameof(AssignFields));
        }

        // 3. View All Assignments
        [HttpGet]
        public async Task<IActionResult> ViewAssignments()
        {
            var assignments = await _context.FieldAssignments
                .Include(a => a.DynamicField)
                .GroupBy(a => new { a.GroupName, a.ScreenType, a.MainCategory, a.Detail })
                .ToListAsync();

            return View(assignments);
        }

        // 4. API to fetch fields for a specific screen/detail combination
        [HttpGet]
        public async Task<IActionResult> GetFieldsForScreen(string detail, string screenType)
        {
            var fields = await _context.FieldAssignments
                .Include(a => a.DynamicField)
                .Where(a => a.Detail == detail && a.ScreenType == screenType)
                .Select(a => a.DynamicField)
                .ToListAsync();

            return Json(fields);
        }

        // 5. API to fetch ALL fields for a specific screen, grouped by GroupName
        [HttpGet]
        public async Task<IActionResult> GetFieldsByScreenTypeOnly(string screenType)
        {
            var assignments = await _context.FieldAssignments
                .Include(a => a.DynamicField)
                .Where(a => a.ScreenType == screenType)
                .ToListAsync();

            var groupedFields = assignments
                .GroupBy(a => string.IsNullOrEmpty(a.GroupName) ? " " : a.GroupName)
                .Select(g => new
                {
                    groupName = g.Key,
                    fields = g.Select(a => new {
                                  id = a.Id, // Treat FieldAssignment Id as the unique field ID for saving
                                  name = a.DynamicField.Name, // Use the actual field name
                                  type = a.DynamicField.Type,
                                  isRequired = a.DynamicField.IsRequired,
                                  options = a.DynamicField.Options
                              })
                              .ToList()
                })
                .ToList();

            return Json(groupedFields);
        }
    }
}
