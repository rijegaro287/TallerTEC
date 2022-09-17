using Backend.Helpers;

namespace Backend.Models;

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
    ///Devuelve todos los empleados de la base de datos
    ///</summary>
    public static Employee[] SelectAllEmployees()
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        return allEmployees;
    }

    ///<summary>
    ///Devuelve un empleado de la base de datos utilizando su id
    ///</summary>
    ///<param name="ID">El id del empleado que se solicita</param>
    public static Employee SelectEmployee(int ID)
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee employee = allEmployees.FirstOrDefault(employee => employee.ID == ID);
        return employee;
    }

    ///<summary>
    ///Devuelve un empleado de la base de datos utilizando su email
    ///</summary>
    ///<param name="email">The email of the employee to be selected.</param>
    public static Employee SelectEmployee(string email)
    {
        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee employee = allEmployees.FirstOrDefault(employee => employee.Email == email);
        return employee;
    }

    ///<summary>
    ///Agrega un empleado a la base de datos
    ///</summary>
    ///<param name="newEmployee">La información del empleado que se creará</param>
    ///<param name="password">La contraseña del empleado que se creará</param>
    public static void InsertEmployee(Employee newEmployee, string password)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        EmployeePassword employeePassword = new EmployeePassword(
            newEmployee.ID, hashedPassword);

        JSONFiles.WriteJSONFile<Employee>(newEmployee, table_path);
        JSONFiles.WriteJSONFile<EmployeePassword>(employeePassword, password_table_path);
    }

    ///<summary>
    ///Actualiza la información de un empleado en la base de datos
    ///</summary>
    ///<param name="ID">El id del empleado que se editará</param>
    ///<param name="newEmployee">La nueva información que tendrá el empleado</param>
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
    ///Elimina un empleado de la base de datos
    ///</summary>
    ///<param name="ID">El id del empleado que se eliminará</param>
    public static void DeleteEmployee(int ID)
    {
        //Delete appointments with the employee as mechanic
        Appointment[] allAppointments = Appointment.SelectAllAppointments();
        Appointment[] appointmentsWithEmployee = allAppointments.Where(appointment => appointment.MechanicID == ID).ToArray();
        foreach (Appointment appointment in appointmentsWithEmployee)
        {
            Appointment.DeleteAppointment(appointment.ID);
        }
        //Delete appointments with the employee as assistant
        allAppointments = Appointment.SelectAllAppointments();
        appointmentsWithEmployee = allAppointments.Where(appointment => appointment.AssistantID == ID).ToArray();
        foreach (Appointment appointment in appointmentsWithEmployee)
        {
            Appointment.DeleteAppointment(appointment.ID);
        }

        Employee[] allEmployees = JSONFiles.ReadJSONFile<Employee[]>(table_path);
        Employee[] newEmployees = allEmployees
            .Where(employee => employee.ID != ID).ToArray<Employee>();
        JSONFiles.WriteOverJSONFile<Employee>(newEmployees, table_path);
    }
}
