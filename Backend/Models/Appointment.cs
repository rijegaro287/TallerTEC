using Backend.Helpers;

namespace Backend.Models;
///
/// This class represents an appointment in the system.
public class Appointment
{
    private static string table_path = "DB/Appointment.json";
    public int ID { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public int AttendedClientID { get; set; }
    public string LicensePlate { get; set; }
    public int BranchID { get; set; }
    public int RequiredService { get; set; }
    public int MechanicID { get; set; }
    public int AssistantID { get; set; }
    public int[] NecessaryParts { get; set; }


    public Appointment(
        int ID,
        string Date,
        string Time,
        int AttendedClientID,
        string LicensePlate,
        int BranchID,
        int RequiredService,
        int MechanicID,
        int AssistantID,
        int[] NecessaryParts)
    {
        this.ID = ID;
        this.Date = Date;
        this.Time = Time;
        this.AttendedClientID = AttendedClientID;
        this.LicensePlate = LicensePlate;
        this.BranchID = BranchID;
        this.RequiredService = RequiredService;
        this.MechanicID = MechanicID;
        this.AssistantID = AssistantID;
        this.NecessaryParts = NecessaryParts;
    }

    /// <summary>
    /// This method selects an appointment from the database.
    /// </summary>
    /// <param name="ID">The ID of the appointment to be selected.</param>
    public static Appointment SelectAppointment(int ID)
    {
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        Appointment appointment = allAppointments.FirstOrDefault(appointment => appointment.ID == ID);
        return appointment;
    }

    /// <summary>
    /// This method selects all appointments from the database.
    /// </summary>
    public static Appointment[] SelectAllAppointments()
    {
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        return allAppointments;
    }

    /// <summary>
    /// This method inserts an appointment into the database.
    /// </summary>
    public static void InsertAppointment(Appointment newAppointment)
    {
        JSONFiles.WriteJSONFile<Appointment>(newAppointment, table_path);
    }

    /// <summary>
    /// This method updates an appointment in the database.
    /// </summary>
    /// <param name="ID">The ID of the appointment to be updated.</param>
    /// <param name="newAppointment">The appointment with new data.</param>
    public static bool UpdateAppointment(int ID, Appointment newAppointment)
    {
        bool wasUpdated = false;
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        Appointment appointment = allAppointments.FirstOrDefault(appointment => appointment.ID == ID);
        if (appointment != null)
        {
            allAppointments[Array.IndexOf(allAppointments, appointment)] = newAppointment;
            wasUpdated = true;
        }
        JSONFiles.WriteOverJSONFile<Appointment>(allAppointments, table_path);
        return wasUpdated;
    }

    /// <summary>
    /// This method deletes an appointment from the database.
    /// </summary>
    /// <param name="ID">The ID of the appointment to be deleted.</param>
    public static void DeleteAppointment(int ID)
    {
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        Appointment[] newAppointments = allAppointments
            .Where(employee => employee.ID != ID).ToArray<Appointment>();
        JSONFiles.WriteOverJSONFile<Appointment>(newAppointments, table_path);
    }

    /// <summary>
    /// This method generates a Bill for an appointment.
    /// </summary>
    /// <param name="ID">The ID of the appointment to be billed.</param>
    public static Bill GenerateBill(int ID)
    {
        Appointment appointment = SelectAppointment(ID);

        Client attendedClient = Client.SelectClient(appointment.AttendedClientID);
        Employee mechanic = Employee.SelectEmployee(appointment.MechanicID);
        Employee assistant = Employee.SelectEmployee(appointment.AssistantID);
        Branch branch = Branch.SelectBranch(appointment.BranchID);
        Service requiredService = Service.SelectService(appointment.RequiredService);
        Product[] necessaryParts = appointment.NecessaryParts
            .Select(productID => Product.SelectProduct(productID)).ToArray<Product>();

        Bill newBill = new Bill(
            appointment.ID,
            branch.ID,
            requiredService.Price,
            necessaryParts.Sum(product => product.Price)
        );

        Bill.InsertBill(newBill);

        return newBill;
    }
}

