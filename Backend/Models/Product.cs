using Backend.Helpers;

namespace Backend.Models;

public class Product
{
    private static string table_path = "DB/Product.json";
    public int ID { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string[] CompatibleModels { get; set; }
    public int Price { get; set; }
    public int PartSupplierID { get; set; }

    public Product(
        int ID,
        string Name,
        string Brand,
        string[] CompatibleModels,
        int Price,
        int PartSupplierID)
    {
        this.ID = ID;
        this.Name = Name;
        this.Brand = Brand;
        this.CompatibleModels = CompatibleModels;
        this.Price = Price;
        this.PartSupplierID = PartSupplierID;
    }

    public static Product SelectProduct(int productID)
    {
        Product[] allProducts = JSONFiles.ReadJSONFile<Product[]>(table_path);
        Product product = allProducts.FirstOrDefault(product => product.ID == productID);

        return product;
    }
}