using Backend.Helpers;

namespace Backend.Models;

public class Employee
{
    private static string table_path = "DB/Employee.json";
    private static string password_table_path = "DB/EmployeePassword.json";
    public int ID { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string BirthDate { get; set; }
    public int Age { get; set; }
    public string Position { get; set; }
    public string StartingDate { get; set; }

    public Employee(
        int ID,
        string Name,
        string LastName,
        string Email,
        string BirthDate,
        int Age,
        string Position,
        string StartingDate)
    {
        this.ID = ID;
        this.Name = Name;
        this.LastName = LastName;
        this.Email = Email;
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

    public static Employee SelectEmployee(string email)
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee employee = allEmployees.FirstOrDefault(employee => employee.Email == email);
        return employee;
    }

    public static void InsertEmployee(Employee newEmployee, string password)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        EmployeePassword employeePassword = new EmployeePassword(
            newEmployee.ID, hashedPassword);

        JSONFiles.WriteJSONFile<Employee>(newEmployee, table_path);
        JSONFiles.WriteJSONFile<EmployeePassword>(employeePassword, password_table_path);
    }

    public static bool UpdateEmployee(int ID, Employee newEmployee)
    {   
        bool wasUpdated = false;
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee employee = allEmployees.FirstOrDefault(employee => employee.ID == ID);
        if(employee != null)
        {
            allEmployees[Array.IndexOf(allEmployees, employee)] = newEmployee;
            wasUpdated = true;
        }
        JSONFiles.WriteOverJSONFile<Employee>(allEmployees, table_path);
        return wasUpdated;
    }

    public static void DeleteEmployee(int ID)
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee[] newEmployees = allEmployees
            .Where(employee => employee.ID != ID).ToArray<Employee>();
        JSONFiles.WriteOverJSONFile<Employee>(newEmployees, table_path);
    }
}
