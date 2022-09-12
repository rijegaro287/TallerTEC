

namespace Server.Models;

public class Bill
{
    public int appointmentID { get; set; }
    public int servicePrice { get; set; }
    public int partsPrice{ get; set; }
    public int totalPrice { get; set; }

    public Bill(int appointmentID, int servicePrice, int partsPrice)
    {
        this.appointmentID = appointmentID;
        this.servicePrice = servicePrice;
        this.partsPrice = partsPrice;
        this.totalPrice = servicePrice + partsPrice;
    }
}