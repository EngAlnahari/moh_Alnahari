using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connString = "Server=.;Database=Awqaf33;Integrated Security=True;TrustServerCertificate=True;";
        
        using (var conn = new SqlConnection(connString))
        {
            conn.Open();
            var cmd = new SqlCommand("SELECT TOP 3 ID, Frist_Name, Second_Name, User_type FROM Users", conn);
            var reader = cmd.ExecuteReader();
            
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== بيانات المستخدمين ===");
            
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["ID"]}, Name: {reader["Frist_Name"]} {reader["Second_Name"]}, Type: {reader["User_type"]}");
            }
        }
    }
}
