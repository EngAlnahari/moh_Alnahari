using Microsoft.Data.SqlClient;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        
        string connString = "Server=.;Database=Awqaf33;Integrated Security=True;TrustServerCertificate=True;";
        
        using var conn = new SqlConnection(connString);
        conn.Open();

        // حذف البيانات القديمة
        string[] deleteCommands = {
            "DELETE FROM Notifications",
            "DELETE FROM Projects", 
            "DELETE FROM Certificates",
            "DELETE FROM WorkOffer",
            "DELETE FROM AdditionalCostType",
            "DELETE FROM sectionTypes",
            "DELETE FROM sections",
            "DELETE FROM Classification",
            "DELETE FROM Almjals",
            "DELETE FROM Users",
            "DELETE FROM Lands"
        };

        foreach (var cmd in deleteCommands)
        {
            try {
                using var c = new SqlCommand(cmd, conn);
                c.ExecuteNonQuery();
            } catch { }
        }

        // إعادة تعيين IDENTITY
        string[] resetCommands = {
            "DBCC CHECKIDENT ('Users', RESEED, 0)",
            "DBCC CHECKIDENT ('Almjals', RESEED, 0)",
            "DBCC CHECKIDENT ('Classification', RESEED, 0)",
            "DBCC CHECKIDENT ('WorkOffer', RESEED, 0)",
            "DBCC CHECKIDENT ('Certificates', RESEED, 0)",
            "DBCC CHECKIDENT ('Projects', RESEED, 0)",
            "DBCC CHECKIDENT ('Notifications', RESEED, 0)",
            "DBCC CHECKIDENT ('Lands', RESEED, 0)"
        };

        foreach (var cmd in resetCommands)
        {
            try {
                using var c = new SqlCommand(cmd, conn);
                c.ExecuteNonQuery();
            } catch { }
        }

        Console.WriteLine("تم حذف البيانات القديمة");

        // إدخال المستخدمين
        var users = new[] {
            ("أحمد", "محمد", "علي", "الفهد", "أحمد", "عميل", "0501234567", "ahmed@test.com", "password123", "ذكر", "سعودي", "متزوج", "1990-05-15", "0501112222"),
            ("فاطمة", "علي", "حسن", "السعود", "فاطمة", "مقاول", "0502345678", "fatima@test.com", "password123", "أنثى", "سعودية", "عزباء", "1995-08-20", "0503334444"),
            ("خالد", "عبدالله", "سلطان", "الراشد", "خالد", "عميل", "0503456789", "khaled@test.com", "password123", "ذكر", "سعودي", "متزوج", "1985-12-10", "0505556666"),
            ("نورة", "سالم", "فهد", "العتيبي", "نورة", "مقاول", "0504567890", "noura@test.com", "password123", "أنثى", "سعودية", "متزوجة", "1992-03-25", "0507778888")
        };

        foreach (var u in users)
        {
            using var cmd = new SqlCommand(
                "INSERT INTO Users (Frist_Name, Second_Name, Third_Name, Fourth_Name, Nickname, User_type, Phone_number1, Email, Pass, Sex, Nationality, MaritalStatus, BirthDate, RelativePhone) " +
                "VALUES (@f1, @f2, @f3, @f4, @nick, @type, @phone, @email, @pass, @sex, @nat, @marital, @birth, @relative)", conn);
            
            cmd.Parameters.AddWithValue("@f1", u.Item1);
            cmd.Parameters.AddWithValue("@f2", u.Item2);
            cmd.Parameters.AddWithValue("@f3", u.Item3);
            cmd.Parameters.AddWithValue("@f4", u.Item4);
            cmd.Parameters.AddWithValue("@nick", u.Item5);
            cmd.Parameters.AddWithValue("@type", u.Item6);
            cmd.Parameters.AddWithValue("@phone", u.Item7);
            cmd.Parameters.AddWithValue("@email", u.Item8);
            cmd.Parameters.AddWithValue("@pass", u.Item9);
            cmd.Parameters.AddWithValue("@sex", u.Item10);
            cmd.Parameters.AddWithValue("@nat", u.Item11);
            cmd.Parameters.AddWithValue("@marital", u.Item12);
            cmd.Parameters.AddWithValue("@birth", DateTime.Parse(u.Item13));
            cmd.Parameters.AddWithValue("@relative", u.Item14);
            
            cmd.ExecuteNonQuery();
        }
        Console.WriteLine("✓ تم إدخال 4 مستخدمين");

        // إدخال المجالات
        var almjals = new[] { "البناء", "الكهرباء", "السباكة", "التكييف", "الدهان" };
        foreach (var name in almjals)
        {
            using var cmd = new SqlCommand("INSERT INTO Almjals (AlmjalName) VALUES (@name)", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
        }
        Console.WriteLine("✓ تم إدخال 5 مجالات");

        // إدخال التصنيفات
        var classifications = new (string name, int almjalId)[] {
            ("بناء سكني", 1), ("بناء تجاري", 1),
            ("تركيبات كهربائية", 2), ("صيانة كهربائية", 2),
            ("تركيب سباكة", 3), ("صيانة سباكة", 3),
            ("تكييف مركزي", 4), ("تكييف سبليت", 4),
            ("دهان داخلي", 5), ("دهان خارجي", 5)
        };
        foreach (var (name, id) in classifications)
        {
            using var cmd = new SqlCommand("INSERT INTO Classification (ClassificationName, AlmjalId) VALUES (@name, @id)", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        Console.WriteLine("✓ تم إدخال 10 تصنيفات");

        // إدخال عروض العمل
        var offers = new (string name, string date, string notes, int userId)[] {
            ("مشروع بناء فيلا سكنية", "2025-12-31", "مشروع بناء فيلا دورين في حي النرجس", 1),
            ("صيانة كهربائية لمجمع", "2025-10-15", "صيانة دورية للمجمع التجاري", 2),
            ("تركيب سباكة لعمارة", "2025-11-30", "تركيب شبكات المياه والصرف", 3),
            ("تكييف مركزي لفيلا", "2025-09-20", "تركيب وحدات تكييف مركزي", 4)
        };
        foreach (var (name, date, notes, userId) in offers)
        {
            using var cmd = new SqlCommand(
                "INSERT INTO WorkOffer (OfferName, EndDate, Notes, UserID) VALUES (@name, @date, @notes, @userId)", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@date", DateTime.Parse(date));
            cmd.Parameters.AddWithValue("@notes", notes);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.ExecuteNonQuery();
        }
        Console.WriteLine("✓ تم إدخال 4 عروض عمل");

        // إدخال المشاريع
        var projects = new (string name, string company, string field, string from, string to, string value, string role, int userId)[] {
            ("فيلا النرجس", "مؤسسة البناء السريع", "بناء سكني", "2025-01-01", "2025-12-31", "2500000 ريال", "مدير مشروع", 1),
            ("صيانة مجمع الرياض", "شركة الصيانة المتكاملة", "صيانة كهربائية", "2025-03-01", "2025-10-15", "500000 ريال", "فني كهرباء", 2)
        };
        foreach (var (name, company, field, from, to, value, role, userId) in projects)
        {
            using var cmd = new SqlCommand(
                "INSERT INTO Projects (ProjName, ProjCompany, ProjField, ProjFrom, ProjTo, ProjValue, ProjRole, UserID) " +
                "VALUES (@name, @company, @field, @from, @to, @value, @role, @userId)", conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@company", company);
            cmd.Parameters.AddWithValue("@field", field);
            cmd.Parameters.AddWithValue("@from", from);
            cmd.Parameters.AddWithValue("@to", to);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.ExecuteNonQuery();
        }
        Console.WriteLine("✓ تم إدخال 2 مشاريع");

        // التحقق من البيانات
        using var verifyCmd = new SqlCommand("SELECT TOP 2 ID, Frist_Name, Second_Name, User_type FROM Users", conn);
        using var reader = verifyCmd.ExecuteReader();
        Console.WriteLine("\n=== التحقق من البيانات ===");
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["Frist_Name"]} {reader["Second_Name"]}, Type: {reader["User_type"]}");
        }

        Console.WriteLine("\n✅ تم إدخال جميع البيانات العربية بنجاح!");
    }
}
