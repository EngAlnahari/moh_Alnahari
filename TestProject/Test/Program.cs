using System.Text;
using Jose;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Test.Data;
using Test.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // Add services to the container.
        var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<DbContextFirst>(options => options.UseSqlServer(connectionstring));
        builder.Services.AddScoped<ITokenCreate, TokenCreate>();
        // ÅÚÏÇÏÇÊ JWT
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

        builder.Services.AddScoped<INotificationService, NotificationService>();


        // ÇÖÇÝå ÇáãÕÇÏÞå
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = builder.Configuration["JWT:Audience"],
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.ASCII.GetBytes(builder.Configuration["JWT:SecretKey"]))
            };
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
            RequestPath = "/Uploads"
        });

        //app.UseStaticFiles(new StaticFileOptions
        //{
        //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "videos")),
        //    RequestPath = "/videos"
        //});
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Pdfs")),
            RequestPath = "/Pdfs"
        });
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        // إنشاء قاعدة البيانات والجداول تلقائياً
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<Test.Data.DbContextFirst>();
            db.Database.EnsureCreated(); // إنشاء قاعدة البيانات والجداول
            
            // إدخال بيانات تجريبية عربية إذا لم تكن موجودة
            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new Test.Models.User { 
                        Frist_Name = "أحمد", Second_Name = "محمد", Third_Name = "علي", Fourth_Name = "الفهد",
                        Nickname = "أحمد", User_type = "عميل", Phone_number1 = "0501234567",
                        Email = "ahmed@test.com", Pass = "password123",
                        Sex = "ذكر", Nationality = "سعودي", MaritalStatus = "متزوج",
                        BirthDate = new DateTime(1990, 5, 15), RelativePhone = "0501112222"
                    },
                    new Test.Models.User { 
                        Frist_Name = "فاطمة", Second_Name = "علي", Third_Name = "حسن", Fourth_Name = "السعود",
                        Nickname = "فاطمة", User_type = "مقاول", Phone_number1 = "0502345678",
                        Email = "fatima@test.com", Pass = "password123",
                        Sex = "أنثى", Nationality = "سعودية", MaritalStatus = "عزباء",
                        BirthDate = new DateTime(1995, 8, 20), RelativePhone = "0503334444"
                    },
                    new Test.Models.User { 
                        Frist_Name = "خالد", Second_Name = "عبدالله", Third_Name = "سلطان", Fourth_Name = "الراشد",
                        Nickname = "خالد", User_type = "عميل", Phone_number1 = "0503456789",
                        Email = "khaled@test.com", Pass = "password123",
                        Sex = "ذكر", Nationality = "سعودي", MaritalStatus = "متزوج",
                        BirthDate = new DateTime(1985, 12, 10), RelativePhone = "0505556666"
                    },
                    new Test.Models.User { 
                        Frist_Name = "نورة", Second_Name = "سالم", Third_Name = "فهد", Fourth_Name = "العتيبي",
                        Nickname = "نورة", User_type = "مقاول", Phone_number1 = "0504567890",
                        Email = "noura@test.com", Pass = "password123",
                        Sex = "أنثى", Nationality = "سعودية", MaritalStatus = "متزوجة",
                        BirthDate = new DateTime(1992, 3, 25), RelativePhone = "0507778888"
                    }
                );
                db.SaveChanges();
                Console.WriteLine("تم إدخال 4 مستخدمين عرب!");
            }
            
            if (!db.Almjals.Any())
            {
                db.Almjals.AddRange(
                    new Test.Models.Almjal { AlmjalName = "البناء" },
                    new Test.Models.Almjal { AlmjalName = "الكهرباء" },
                    new Test.Models.Almjal { AlmjalName = "السباكة" },
                    new Test.Models.Almjal { AlmjalName = "التكييف" },
                    new Test.Models.Almjal { AlmjalName = "الدهان" }
                );
                db.SaveChanges();
                Console.WriteLine("تم إدخال 5 مجالات!");
            }
            
            if (!db.Classification.Any())
            {
                db.Classification.AddRange(
                    new Test.Models.Classification { ClassificationName = "بناء سكني", AlmjalId = 1 },
                    new Test.Models.Classification { ClassificationName = "بناء تجاري", AlmjalId = 1 },
                    new Test.Models.Classification { ClassificationName = "تركيبات كهربائية", AlmjalId = 2 },
                    new Test.Models.Classification { ClassificationName = "صيانة كهربائية", AlmjalId = 2 },
                    new Test.Models.Classification { ClassificationName = "تركيب سباكة", AlmjalId = 3 },
                    new Test.Models.Classification { ClassificationName = "صيانة سباكة", AlmjalId = 3 }
                );
                db.SaveChanges();
                Console.WriteLine("تم إدخال 6 تصنيفات!");
            }
            
            if (!db.WorkOffer.Any())
            {
                db.WorkOffer.AddRange(
                    new Test.Models.WorkOffer { 
                        OfferName = "مشروع بناء فيلا سكنية", 
                        EndDate = new DateTime(2025, 12, 31),
                        Notes = "مشروع بناء فيلا دورين في حي النرجس",
                        UserID = 1
                    },
                    new Test.Models.WorkOffer { 
                        OfferName = "صيانة كهربائية لمجمع", 
                        EndDate = new DateTime(2025, 10, 15),
                        Notes = "صيانة دورية للمجمع التجاري",
                        UserID = 2
                    }
                );
                db.SaveChanges();
                Console.WriteLine("تم إدخال 2 عروض عمل!");
            }
            
            Console.WriteLine("تم إنشاء قاعدة البيانات والجداول بنجاح!");
        }
        app.Run();
    }
}