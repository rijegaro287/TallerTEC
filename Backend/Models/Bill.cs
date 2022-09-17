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

    /// <summary>
    /// Devuelve una factura de la base de datos
    /// </summary>
    /// <param name="appointmentID">El id de la cita generada</param>
    public static Bill selectBill(int appointmentID)
    {
        Bill[] allBills = JSONFiles.ReadJSONFile<Bill[]>(table_path);
        Bill bill = allBills.FirstOrDefault(bill => bill.appointmentID == appointmentID);
        return bill;
    }

    /// <summary>
    /// Devuelve todas las facturas de la base de datos
    /// </summary>
    public static Bill[] selectAllBills()
    {
        Bill[] allBills = JSONFiles.ReadJSONFile<Bill[]>(table_path);
        return allBills;
    }

    /// <summary>
    /// Agrega una factura a la base de datos
    /// </summary>
    /// <param name="bill">La información de la factura que se creará</param>
    public static void InsertBill(Bill newBill)
    {
        JSONFiles.WriteJSONFile<Bill>(newBill, table_path);
    }
}