using Backend.Helpers;

namespace Backend.Models;

///<summary>
/// Clients' passwords.
///</summary>
public class ClientPassword
{
    private static string table_path = "DB/ClientPassword.json";
    public int ClientID { get; set; }
    public string Password { get; set; }

    public ClientPassword(int ClientID, string Password)
    {
        this.ClientID = ClientID;
        this.Password = Password;
    }

    ///<summary>
    /// Returns a client's password.
    ///</summary>
    private static string SelectPassword(int ClientID)
    {
        ClientPassword[] allPasswords = JSONFiles.ReadJSONFile<ClientPassword[]>(table_path);
        ClientPassword password = allPasswords.FirstOrDefault(password => password.ClientID == ClientID);

        return password.Password;
    }

    ///<summary>
    /// Verifies if a password is correct.
    ///</summary>
    ///<param name="email">The email of the client.</param>
    ///<param name="passwordInput">The password input by the user.</param>
    public static bool ValidatePassword(string email, string passwordInput)
    {
        Client client = Client.SelectClient(email);

        if (client != null)
        {
            string hashedPassword = SelectPassword(client.ID);

            return BCrypt.Net.BCrypt.Verify(passwordInput, hashedPassword); ;
        }
        else
        {
            return false;
        }
    }
}
