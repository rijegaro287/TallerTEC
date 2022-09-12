using Server.Helpers;

namespace Server.Models;

public class Employee
{
    private static string table_path = "DB/Employee.json";
    public int ID { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string BirthDate { get; set; }
    public int Age { get; set; }
    public string Position { get; set; }
    public string StartingDate { get; set; }

    public Employee(
        int ID,
        string Name,
        string LastName,
        string BirthDate,
        int Age,
        string Position,
        string StartingDate)
    {
        this.ID = ID;
        this.Name = Name;
        this.LastName = LastName;
        this.BirthDate = BirthDate;
        this.Age = Age;
        this.Position = Position;
        this.StartingDate = StartingDate;
    }

    public static Employee[] SelectAllEmployees()
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        return allEmployees;
    }

    public static Employee SelectEmployee(int ID)
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee employee = allEmployees.FirstOrDefault(employee => employee.ID == ID);
        return employee;
    }

    public static void InsertEmployee(Employee newEmployee)
    {
        JSONFiles.WriteJSONFile<Employee>(newEmployee, table_path);
    }
}
