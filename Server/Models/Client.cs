using Server.Helpers;

namespace Server.Models;

public class Client : Person
{
    private static string table_path = "DB/Client.json";
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public  string Address { get; set; }

    public Client(
        int ID,
        string Name,
        string LastName,
        string Password,
        string Email,
        int PhoneNumber,
        string Address) : base(ID, Name, LastName, Password)
    {
        this.Email = Email;
        this.PhoneNumber = PhoneNumber;
        this.Address = Address;
    }

    public static Client SelectClient(int id)
    {
        Client[] allClients = JSONFiles.ReadJSONFile<Client[]>(table_path);
        Client client = allClients.FirstOrDefault(client => client.ID == id);
        return client;
    }

    public static Client[] SelectAllClients()
    {
        Client[] allClients = JSONFiles.ReadJSONFile<Client[]>(table_path);
        return allClients;
    }

    // !Return bool
    public static void InsertClient(Client newClient)
    {
        JSONFiles.WriteJSONFile<Client>(newClient, table_path);
    }

    public static bool UpdateClient(int ID, Client newClient)
    {
        bool wasUpdated = false;
        Client[] allClients = JSONFiles.ReadJSONFile<Client[]>(table_path);
        Client client = allClients.FirstOrDefault(client => client.ID == ID);
        if(client != null)
        {
            allClients[Array.IndexOf(allClients, client)] = newClient;
            wasUpdated = true;
        }
        JSONFiles.WriteOverJSONFile<Client>(allClients, table_path);
        return wasUpdated;
    }

    public static void DeleteClient(int ID)
    {
        Client[] allClients = JSONFiles.ReadJSONFile<Client[]>(table_path);
        Client client = allClients.FirstOrDefault(client => client.ID == ID);
        if(client != null)
        {
            allClients = allClients.Where(client => client.ID != ID).ToArray();
        }
        JSONFiles.WriteOverJSONFile<Client>(allClients, table_path);
    }

    public static bool UpdatePassword(string email, string oldPassword, string newPassword, string confirmPassword)
    {
        bool wasUpdated = false;
        Client[] allClients = JSONFiles.ReadJSONFile<Client[]>(table_path);
        Client client = allClients.FirstOrDefault(client => client.Email == email);
        if(client != null)
        {
            if(client.UpdatePassword(oldPassword, newPassword, confirmPassword))
            {
                allClients[Array.IndexOf(allClients, client)] = client;
                JSONFiles.WriteOverJSONFile<Client>(allClients, table_path);
                wasUpdated = true;
            }
        }
        return wasUpdated;
    }
}


