using Backend.Helpers;

namespace Backend.Models;

public class Service
{
    private static string table_path = "DB/Service.json";
    public int ID { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int EstimatedDuration { get; set; }

    public Service(
        int ID,
        string Name,
        int Price,
        int EstimatedDuration)
    {
        this.ID = ID;
        this.Name = Name;
        this.Price = Price;
        this.EstimatedDuration = EstimatedDuration;
    }

    /// <summary>
    /// Devuelve un servicio de la base de datos utilizando su id
    /// </summary>
    /// <param name="ID">El id del servicio que se solicita</param>
    public static Service SelectService(int serviceID)
    {
        Service[] allServicees = JSONFiles.ReadJSONFile<Service[]>(table_path);
        Service Service = allServicees.FirstOrDefault(service => service.ID == serviceID);

        return Service;
    }
}