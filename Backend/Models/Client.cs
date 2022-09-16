using Backend.Helpers;

namespace Backend.Models;

///<summary>
/// Represents a client.
///</summary>
public class Client : Person
{
    private static string table_path = "DB/Client.json";
    private static string password_table_path = "DB/ClientPassword.json";

    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public string Address { get; set; }

    public Client(
        int ID,
        string Name,
        string LastName,
        string Email,
        int PhoneNumber,
        string Address) : base(ID, Name, LastName)
    {
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.Address = Address;
    }
    ///<summary>
    /// Returns a client
    ///</summary>
    ///<param name="clientID">The ID of the client to be selected.</param>
    public static Client SelectClient(int id)
    {
        Client[] allClients = JSONFiles.ReadJSONFile<Client[]>(table_path);
        Client client = allClients.FirstOrDefault(client => client.ID == id);
        return client;
    }

    ///<summary>
    /// Selects a client by email.
    ///</summary>
    ///<parma name="email">The email of the client to be selected.</param>
    public static Client SelectClient(string email)
    {
        Client[] allClients = JSONFiles.ReadJSONFile<Client[]>(table_path);
        Client client = allClients.FirstOrDefault(client => client.Email == email);
        return client;
    }

    ///<summary>
    /// Selects all clients.
    ///</summary>
    public static Client[] SelectAllClients()
    {
        Client[] allClients = JSONFiles.ReadJSONFile<Client[]>(table_path);
        return allClients;
    }

    ///<summary>
    /// Inserts a client.
    ///</summary>
    public static async Task InsertClientAsync(Client newClient)
    {
        string randomPassword = GenerateRandomPassword();
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(randomPassword);

        ClientPassword employeePassword = new ClientPassword(newClient.ID,
                                                             hashedPassword);

        // await EmailSender.SendEmailAsync(newClient.Name,
        //                                  newClient.Email,
        //                                  "TallerTEC - Password",
        //                                  $"Your password is: {randomPassword}");

        JSONFiles.WriteJSONFile<Client>(newClient, table_path);
        JSONFiles.WriteJSONFile<ClientPassword>(employeePassword, password_table_path);
    }

    ///<summary>
    /// Updates a client.
    ///</summary>
    ///<param name="ID">The ID of the client to be updated.</param>
    ///<param name="newClient">The new client.</param>
    public static bool UpdateClient(int ID, Client newClient)
    {
        bool wasUpdated = false;
        Client[] allClients = JSONFiles.ReadJSONFile<Client[]>(table_path);
        Client client = allClients.FirstOrDefault(client => client.ID == ID);
        if (client != null)
        {
            allClients[Array.IndexOf(allClients, client)] = newClient;
            wasUpdated = true;
        }
        JSONFiles.WriteOverJSONFile<Client>(allClients, table_path);
        return wasUpdated;
    }

    ///<summary>
    /// Deletes a client.
    ///</summary>
    ///<param name="ID">The ID of the client to be deleted.</param>
    public static void DeleteClient(int ID)
    {   
        //Delete appointments with the client
        Appointment[] allAppointments = Appointment.SelectAllAppointments();
        Appointment[] appointmentsWithClient = allAppointments.Where(appointment => appointment.AttendedClientID == ID).ToArray();
        foreach (Appointment appointment in appointmentsWithClient)
        {
            Appointment.DeleteAppointment(appointment.ID);
        }

        //Delete client
        Client[] allClients = Client.SelectAllClients();
        Client client = Client.SelectClient(ID);
        if (client != null)
        {
            allClients = allClients.Where(client => client.ID != ID).ToArray();
        }
        JSONFiles.WriteOverJSONFile<Client>(allClients, table_path);
    }

    ///<summary>
    /// Generates a random password.
    ///</summary>
    private static string GenerateRandomPassword()
    {
        int passwordLength = 12;
        string password = "";

        Random random = new Random();
        for (int i = 0; i < passwordLength; i++)
        {
            password += (char)random.Next(35, 126);
        }

        return password;
    }
}


