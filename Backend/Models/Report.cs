namespace Backend.Models;
public class Report
{
    // public Report(){}

    public static void Sells(string fromDate, string toDate)
    {
        DateTime d1, d2;
        if (DateTime.TryParse(fromDate, out d1) && DateTime.TryParse(toDate, out d2) && d2 <= d1)
        {
            Appointment[] allAppointments = Appointment.SelectAllAppointments();
            //leave only appointments between the dates
            Appointment[] appointmentsBetweenDates = allAppointments.Where(appointment => 
                DateTime.Parse(appointment.Date) >= d1 &&
                DateTime.Parse(appointment.Date) <= d2).ToArray();
            
            //get all bills between the dates
            Bill[] allBills = Bill.selectAllBills();
            Bill[] billsBetweenDates = allBills.Where(bill => 
                appointmentsBetweenDates.Any(appointment => appointment.ID == bill.appointmentID)).ToArray();
            
            //Falta ordenarlo por sucursal

        }
        else
        {
            Console.WriteLine("Invalid date format");
        }
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
}