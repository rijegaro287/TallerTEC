using Backend.Helpers;
namespace Backend.Models;

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

    public static Bill selectBill(int appointmentID)
    {
        Bill[] allBills = JSONFiles.ReadJSONFile<Bill[]>(table_path);
        Bill bill = allBills.FirstOrDefault(bill => bill.appointmentID == appointmentID);
        return bill;
    }

    public static Bill[] selectAllBills()
    {
        Bill[] allBills = JSONFiles.ReadJSONFile<Bill[]>(table_path);
        return allBills;
    }

    public static void InsertBill(Bill newBill)
    {
        JSONFiles.WriteJSONFile<Bill>(newBill, table_path);
    }
}