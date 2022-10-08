using crud_interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

bool running = true;
while (running)
{
    Console.WriteLine("Create a Department:");
    Console.WriteLine("Enter department name");
    string name = Console.ReadLine().ToString();
    Console.WriteLine("Enter mgr ssn");
    int mgr = int.Parse(Console.ReadLine());
    Procedures.CreateDepartment(name, mgr);
}

public class Procedures
{
    public static void CreateDepartment(string name = "Evil Corp", int? mgr = 666884444)
    {
        if (name == null)
        {
            Console.WriteLine("Name was null");
            return;
        }
        if (mgr == null)
        {
            Console.WriteLine("Mgr was null");
            return;
        }
        using (DbContext _context = new Company_OnlineContext())
        {
            _context.Database.OpenConnection();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_CreateDepartment";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@DName", name));
                command.Parameters.Add(new SqlParameter("@MgrSSN", mgr));
                Console.WriteLine(command.ExecuteNonQuery());
            }
        }
    }
}