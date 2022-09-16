using Backend.Helpers;

namespace Backend.Models;

///<summary>
/// Represents a branch from the company.
///</summary>
public class Branch
{
    private static string table_path = "DB/Branch.json";
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int PhoneNumber { get; set; }
    public int[] Employees { get; set; }
    public string OpeningDate { get; set; }
    public string ManagerStartingDate { get; set; }
    public int[] Services { get; set; }

    public Branch(
        int ID,
        string Name,
        string Location,
        int PhoneNumber,
        int[] Employees,
        string OpeningDate,
        string ManagerStartingDate,
        int[] Services)
    {
        this.ID = ID;
        this.Name = Name;
        this.Location = Location;
        this.PhoneNumber = PhoneNumber;
        this.Employees = Employees;
        this.OpeningDate = OpeningDate;
        this.ManagerStartingDate = ManagerStartingDate;
        this.Services = Services;
    }

    public static Branch SelectBranch(int branchID)
    {
        Branch[] allBranches = JSONFiles.ReadJSONFile<Branch[]>(table_path);
        Branch branch = allBranches.FirstOrDefault(branch => branch.ID == branchID);

        return branch;
    }
}