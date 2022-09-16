using Backend.Helpers;

namespace Backend.Models;

///<summary>
/// Clients' passwords.
///</summary>
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

    ///<summary>
    /// Returns a client's password.
    ///</summary>
    ///<param name="EmployeeID">The ID of the employee.</param>
    private static string SelectPassword(int employeeID)
    {
        EmployeePassword[] allPasswords = JSONFiles.ReadJSONFile<EmployeePassword[]>(table_path);
        EmployeePassword password = allPasswords.FirstOrDefault(password => password.EmployeeID == employeeID);

        return password.Password;
    }

    ///<summary>
    /// Verifies if a password is correct.
    ///</summary>
    ///<param name="email">The email of the employee.</param>
    ///<param name="passwordInput">The password input by the user.</param>
    public static bool ValidatePassword(string email, string passwordInput)
    {
        Employee employee = Employee.SelectEmployee(email);

        if (employee != null)
        {
            string hashedPassword = SelectPassword(employee.ID);

            return BCrypt.Net.BCrypt.Verify(passwordInput, hashedPassword); ;
        }
        else
        {
            return false;
        }
    }
}
