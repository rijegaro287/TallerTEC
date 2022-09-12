using Server.Helpers;

namespace Server.Models;

public class Appointment 
{
    private static string table_path = "DB/Appointment.json";
    public int ID { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public int AttendedClientID { get; set; }
    public string LicensePlate { get; set; }
    public int MechanicID { get; set; }
    public int AssistantID { get; set; }

    // public Branch SelectedBranch { get; set; }
    // public Service ReqiuredService { get; set; }
    // public List<Product> NecessaryParts { get; set; }


    public Appointment(
        int ID,
        string Date,
        string Time,
        int AttendedClientID,
        string LicensePlate,
        // Branch SelectedBranch,
        // Service ReqiuredService,
        int MechanicID,
        int AssistantID)
    {
        this.ID = ID;
        this.Date = Date;
        this.Time = Time;
        this.AttendedClientID = AttendedClientID;
        this.LicensePlate = LicensePlate;
        // this.SelectedBranch = SelectedBranch;
        // this.RequiredService = RequiredService;
        this.MechanicID = MechanicID;
        this.AssistantID = AssistantID;
    }

    public static Appointment SelectAppointment(int ID)
    {
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        Appointment appointment = allAppointments.FirstOrDefault(appointment => appointment.ID == ID);
        return appointment;
    }

    public static Appointment[] SelectAllAppointments()
    {
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        return allAppointments;
    }

    // !Return bool
    public static void InsertAppointment(Appointment newAppointment)
    {
        JSONFiles.WriteJSONFile<Appointment>(newAppointment, table_path);
    }

    public static bool UpdateAppointment(int ID, Appointment newAppointment)
    {
        bool wasUpdated = false;
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        Appointment appointment = allAppointments.FirstOrDefault(appointment => appointment.ID == ID);
        if(appointment != null)
        {
            allAppointments[Array.IndexOf(allAppointments, appointment)] = newAppointment;
            wasUpdated = true;
        }
        JSONFiles.WriteOverJSONFile<Appointment>(allAppointments, table_path);
        return wasUpdated;
    }

    public static void DeleteAppointment(int ID)
    {
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        Appointment[] newAppointments = allAppointments
            .Where(employee => employee.ID != ID).ToArray<Appointment>();
        JSONFiles.WriteOverJSONFile<Appointment>(newAppointments, table_path);
    }

    public static Bill GenerateBill(int ID)
    {
        Appointment appointment = SelectAppointment(ID);
        int servicePrice = 1000;
        int partsPrice = 1000;
        int totalPrice = servicePrice + partsPrice;
        Bill bill = new Bill(
            appointment.ID,
            servicePrice,
            partsPrice);
        return bill;

    }
}


