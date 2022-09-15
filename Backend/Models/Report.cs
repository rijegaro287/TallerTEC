namespace Backend.Models;

public class Report
{
    public static Appointment[] SalesPerBranch(string fromDate, string toDate)
    {   
        DateTime DfromDate = Convert.ToDateTime(fromDate);
        DateTime DtoDate = Convert.ToDateTime(toDate);
            Appointment[] allAppointments = Appointment.SelectAllAppointments();
            //leave only appointments between the dates
            Appointment[] appointmentsBetweenDates = allAppointments.Where(appointment => 
                Convert.ToDateTime(fromDate) >= DfromDate &&
                Convert.ToDateTime(toDate) <= DtoDate).ToArray();
        
        int[] appointmentsID = appointmentsBetweenDates.Select(appointment => appointment.ID).ToArray();
        Bill[] billBetweenDates =  new Bill[appointmentsID.Length];
        for(int i = 0; i < appointmentsID.Length; i++)
        {
            billBetweenDates[i] = Bill.selectBill(appointmentsBetweenDates[i].ID);
        }

        //totalSalesPerBranch(billBetweenDates);
        
        return appointmentsBetweenDates;
    }

    public static Dictionary<string,int> TopFrequentVehicles()
    {
        Appointment [] allAppointments = Appointment.SelectAllAppointments();
        string[] allLicensePlates = allAppointments.Select(appointment => appointment.LicensePlate).ToArray();
        Dictionary<string, int> mostFrequentVehicles = getMostFrequentVehicles(allLicensePlates);
        return mostFrequentVehicles;

    }

    public static Dictionary<int,int> TopFrequentClients(){
        Appointment [] allAppointments = Appointment.SelectAllAppointments();
        int[] allClientsIDs = allAppointments.Select(appointment => appointment.AttendedClientID).ToArray();
        Dictionary<int, int> mostFrequentClients = getMostFrequentClients(allClientsIDs);
        return mostFrequentClients;
    }

    private static Dictionary<string,int> getMostFrequentVehicles(string[] allLicensePlates)
    {
        Dictionary<string, int> mostFrequentVehicles = new Dictionary<string, int>();
        foreach (string licensePlate in allLicensePlates)
        {
            if (mostFrequentVehicles.ContainsKey(licensePlate))
            {
                mostFrequentVehicles[licensePlate] += 1;
            }
            else
            {
                mostFrequentVehicles.Add(licensePlate, 1);
            }
        }
        return mostFrequentVehicles;
    }

    private static Dictionary<int,int> getMostFrequentClients(int[] allClientsIDs)
    {
        Dictionary<int, int> mostFrequentClients = new Dictionary<int, int>();
        foreach (int clientID in allClientsIDs)
        {
            if (mostFrequentClients.ContainsKey(clientID))
            {
                mostFrequentClients[clientID] += 1;
            }
            else
            {
                mostFrequentClients.Add(clientID, 1);
            }
        }
        return mostFrequentClients;
    }

    //private static totalSalesPerBranch(Bill[] billBetweenDates)
    //{
        
    //}
}