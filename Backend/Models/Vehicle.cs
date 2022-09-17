using Backend.Helpers;

namespace Backend.Models;

public class Vehicle
{
    private static string table_path = "DB/Vehicle.json";
    public string LicensePlate { get; set; }
    public string[] color { get; set; }

    public Vehicle(
        string LicensePlate,
        string[] color)
    {
        this.LicensePlate = LicensePlate;
        this.color = color;
    }

    ///<summary>
    ///Devuelve un vehiculo
    ///</summary>
    ///<param name="LicensePlate">The LicensePlate of the vehicle.</param>
    public static Vehicle SelectClient(string LicensePlate)
    {
        Vehicle[] allVehicles = JSONFiles.ReadJSONFile<Vehicle[]>(table_path);
        Vehicle vehicle = allVehicles.FirstOrDefault(vehicle => vehicle.LicensePlate == LicensePlate);
        return vehicle;
    }

    ///<summary>
    ///Returns all vehicles
    ///</summary>
    public static Vehicle[] SelectAllVehicles()
    {
        Vehicle[] allVehicles = JSONFiles.ReadJSONFile<Vehicle[]>(table_path);
        return allVehicles;
    }
}