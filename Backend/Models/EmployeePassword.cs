using Backend.Helpers;

namespace Backend.Models;

public class EmployeePassword
{
    private static string table_path = "DB/EmployeePassword.json";
    public int EmployeeID { get; set; }
    public string Password { get; set; }

    public EmployeePassword(int EmployeeID, string Password)
    {
        this.EmployeeID = EmployeeID;
        this.Password = Password;
    }

    private static string SelectPassword(int employeeID)
    {
        EmployeePassword[] allPasswords = JSONFiles.ReadJSONFile<EmployeePassword[]>(table_path);
        EmployeePassword password = allPasswords.FirstOrDefault(password => password.EmployeeID == employeeID);

        return password.Password;
    }
    public static bool ValidatePassword(string email, string password)
    {
        Employee employee = Employee.SelectEmployee(email);

        if (employee != null)
        {
            string employeePassword = SelectPassword(employee.ID);

            return password == employeePassword;
        }
        else
        {
            return false;
        }
    }
}
