using Backend.Helpers;

namespace Backend.Models;

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
    /// Devuelve una cita de la base de datos
    /// </summary>
    /// <param name="ID">El id de la cita que se enviará</param>
    public static Appointment SelectAppointment(int ID)
    {
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        Appointment appointment = allAppointments.FirstOrDefault(appointment => appointment.ID == ID);
        return appointment;
    }

    /// <summary>
    /// Devuelve todas las citas de la base de datos
    /// </summary>
    public static Appointment[] SelectAllAppointments()
    {
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        return allAppointments;
    }

    /// <summary>
    /// Agregar una cita a la base de datos
    /// </summary>
    public static void InsertAppointment(Appointment newAppointment)
    {
        JSONFiles.WriteJSONFile<Appointment>(newAppointment, table_path);
    }

    /// <summary>
    /// Actualiza una cita de la base de datos
    /// </summary>
    /// <param name="ID">El id de la cita que se actualizará</param>
    /// <param name="newAppointment">La nueva información de la cita</param>
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
    /// Elimina un cita de la base de datos
    /// </summary>
    /// <param name="ID">El id de la cita que se eliminará</param>
    public static void DeleteAppointment(int ID)
    {
        Appointment[] allAppointments = JSONFiles.ReadJSONFile<Appointment[]>(table_path);
        Appointment[] newAppointments = allAppointments
            .Where(employee => employee.ID != ID).ToArray<Appointment>();
        JSONFiles.WriteOverJSONFile<Appointment>(newAppointments, table_path);
    }

    /// <summary>
    /// Genera la factura de una cita
    /// </summary>
    /// <param name="ID">El id de la cita que se generará</param>
    public static void GenerateBill(int ID)
    {
        Appointment appointment = SelectAppointment(ID);

        HandlerPDF.buildBillPDF(appointment);
    }
}

