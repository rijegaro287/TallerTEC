using Backend.Helpers;

namespace Backend.Models;

///<summary>
/// Represetns an employee.
///</summary>
public class Employee : Person
{
    private static string table_path = "DB/Employee.json";
    private static string password_table_path = "DB/EmployeePassword.json";
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
        string StartingDate) : base(ID, Name, LastName)
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

    ///<summary>
    ///Returns all employees
    ///</summary>
    public static Employee[] SelectAllEmployees()
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        return allEmployees;
    }

    ///<summary>
    ///Returns an employee
    ///</summary>
    ///<param name="ID">The ID of the employee to be selected.</param>
    public static Employee SelectEmployee(int ID)
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee employee = allEmployees.FirstOrDefault(employee => employee.ID == ID);
        return employee;
    }

    ///<summary>
    ///Returns an employee
    ///</summary>
    ///<param name="email">The email of the employee to be selected.</param>
    public static Employee SelectEmployee(string email)
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee employee = allEmployees.FirstOrDefault(employee => employee.Email == email);
        return employee;
    }

    ///<summary>
    ///Inserts an employee in the database.
    ///</summary>
    ///<param name="newEmployee">The employee to be inserted.</param>
    ///<param name="password">The password of the employee to be inserted.</param>
    public static void InsertEmployee(Employee newEmployee, string password)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        EmployeePassword employeePassword = new EmployeePassword(
            newEmployee.ID, hashedPassword);

        JSONFiles.WriteJSONFile<Employee>(newEmployee, table_path);
        JSONFiles.WriteJSONFile<EmployeePassword>(employeePassword, password_table_path);
    }

    ///<summary>
    ///Updates an employee in the database.
    ///</summary>
    ///<param name="ID">The ID of the employee to be updated.</param>
    ///<param name=newEmployee>The employee to be updated.</param>
    public static bool UpdateEmployee(int ID, Employee newEmployee)
    {
        bool wasUpdated = false;
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee employee = allEmployees.FirstOrDefault(employee => employee.ID == ID);
        if (employee != null)
        {
            allEmployees[Array.IndexOf(allEmployees, employee)] = newEmployee;
            wasUpdated = true;
        }
        JSONFiles.WriteOverJSONFile<Employee>(allEmployees, table_path);
        return wasUpdated;
    }

    ///<summary>
    ///Deletes an employee from the database.
    ///</summary>
    ///<param name="ID">The ID of the employee to be deleted.</param>
    public static void DeleteEmployee(int ID)
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee[] newEmployees = allEmployees
            .Where(employee => employee.ID != ID).ToArray<Employee>();
        JSONFiles.WriteOverJSONFile<Employee>(newEmployees, table_path);
    }
}
