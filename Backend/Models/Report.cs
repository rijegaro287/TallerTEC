namespace Backend.Models;

public class Report
{   
    ///<summary>
    ///Returns a dictionary(branchID, totalSales) with the sales per branch between two given dates
    ///</summary>
    ///<param name="startDate">The start date of the report.</param>
    ///<param name="endDate">The end date of the report.</param>
    public static Dictionary<int,int> SalesPerBranch(string fromDate, string toDate)
    {   
        DateTime DfromDate = Convert.ToDateTime(fromDate);
        DateTime DtoDate = Convert.ToDateTime(toDate);
            Appointment[] allAppointments = Appointment.SelectAllAppointments();
            //leave only appointments between the dates
            Appointment[] appointmentsBetweenDates = allAppointments.Where(appointment => 
                Convert.ToDateTime(fromDate) >= DfromDate &&
                Convert.ToDateTime(toDate) <= DtoDate).ToArray();
        
        int[] appointmentsID = appointmentsBetweenDates.Select(appointment => appointment.ID).ToArray();
        List<Bill> billBetweenDates = new List<Bill>();
        for(int i = 0; i < appointmentsID.Length; i++)
        {
            Bill bill = Bill.selectBill(appointmentsID[i]);
            if(bill != null)
            {
                billBetweenDates.Add(bill);
            }
        }
        Dictionary<int, int> salesPerBranch = totalSalesPerBranch(billBetweenDates);
        HandlerPDF.buildSalesPerBranchPDF("Ventas por Sucursal", fromDate, toDate, salesPerBranch);
        return salesPerBranch;
    }

    ///<summary>
    ///Returns a dictionary(LicensePlate, numOfAppoinments) with the top 10 most frequent vehicles
    ///</summary>

    public static Dictionary<string,int> TopFrequentVehicles()
    {
        Appointment [] allAppointments = Appointment.SelectAllAppointments();
        string[] allLicensePlates = allAppointments.Select(appointment => appointment.LicensePlate).ToArray();
        Dictionary<string, int> mostFrequentVehicles = getMostFrequentVehicles(allLicensePlates);
        HandlerPDF.buildTopVehiclesPDF("Top Vehiculos", mostFrequentVehicles);
        return mostFrequentVehicles;

    }
    ///<summary>
    ///Returns a dictionary(ClientID,numOfAppoinments) with the top 10 most frequent clients
    ///</summary>

    public static Dictionary<int,int> TopFrequentClients(){
        Appointment [] allAppointments = Appointment.SelectAllAppointments();
        int[] allClientsIDs = allAppointments.Select(appointment => appointment.AttendedClientID).ToArray();
        Dictionary<int, int> mostFrequentClients = getMostFrequentClients(allClientsIDs);
        HandlerPDF.bulidTopClientsPDF("Top Clientes", mostFrequentClients);
        return mostFrequentClients;
    }

    ///<summary>
    ///Gets the top most frecuent vehicles
    ///</summary>
    ///<param name="allLicensePlates">An array with all the license plates of the appointments</param>
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

    ///<summary>
    ///Gets the top most frecuent clients
    ///</summary>
    ///<param name="allClientsIDs">An array with all the clients IDs of the appointments</param>
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

    ///<summary>
    ///Returns a dictionary(branchID, totalSales) with the total sales per branch
    ///</summary>
    ///<param name="billBetweenDates">A List with all the bills between the dates</param>
    private static Dictionary<int, int> totalSalesPerBranch(List<Bill> billBetweenDates)
    {
        Dictionary<int, int> salesPerBranch = new Dictionary<int, int>();
        foreach (Bill bill in billBetweenDates)
        {
            if (salesPerBranch.ContainsKey(bill.branchID))
            {
                salesPerBranch[bill.branchID] += bill.totalPrice;
            }
            else
            {
                salesPerBranch.Add(bill.branchID, bill.totalPrice);
            }
        }
        return salesPerBranch;
    }
}