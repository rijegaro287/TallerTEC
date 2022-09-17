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

    ///<summary>
    /// Devuelve la contraseña de un empleado
    ///</summary>
    ///<param name="EmployeeID">El id del empleado</param>
    private static string SelectPassword(int employeeID)
    {
        EmployeePassword[] allPasswords = JSONFiles.ReadJSONFile<EmployeePassword[]>(table_path);
        EmployeePassword password = allPasswords.FirstOrDefault(password => password.EmployeeID == employeeID);

        return password.Password;
    }

    ///<summary>
    /// Verifica si la contraseña ingresada es correcta
    ///</summary>
    ///<param name="email">El email del empleado</param>
    ///<param name="passwordInput">La contraseña ingresada por el usuario</param>
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
