using Backend.Helpers;
namespace Backend.Models;

///<summary>
/// Represents a bill in the system. Every bill comes from an appointment.
///</summary>
public class Bill
{
    private static string table_path = "DB/Bill.json";
    public int appointmentID { get; set; }
    public int servicePrice { get; set; }
    public int partsPrice { get; set; }
    public int totalPrice { get; set; }
    public int branchID { get; set; }


    public Bill(int appointmentID, int branchID, int servicePrice, int partsPrice)
    {
        this.branchID = branchID;
        this.appointmentID = appointmentID;
        this.servicePrice = servicePrice;
        this.partsPrice = partsPrice;
        this.totalPrice = servicePrice + partsPrice;
    }

    /// <summary>
    /// This method selects a bill from the database.
    /// </summary>
    /// <param name="appointmentID">The ID of the appointment to be selected.</param>
    public static Bill selectBill(int appointmentID)
    {
        Bill[] allBills = JSONFiles.ReadJSONFile<Bill[]>(table_path);
        Bill bill = allBills.FirstOrDefault(bill => bill.appointmentID == appointmentID);
        return bill;
    }

    /// <summary>
    /// This method selects all bills from the database.
    /// </summary>
    public static Bill[] selectAllBills()
    {
        Bill[] allBills = JSONFiles.ReadJSONFile<Bill[]>(table_path);
        return allBills;
    }

    /// <summary>
    /// This method inserts a bill into the database.
    /// </summary>
    /// <param name="bill">The bill to be inserted.</param>
    public static void InsertBill(Bill newBill)
    {
        JSONFiles.WriteJSONFile<Bill>(newBill, table_path);
    }
}