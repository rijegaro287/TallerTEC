namespace Backend.Models;

/// <summary>
/// Clase abstracta que representa a una persona, ya sea empleado o cliente
/// </summary>
public abstract class Person
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }

    public Person(int ID, string Name, string LastName)
    {
        this.ID = ID;
        this.Name = Name;
        this.LastName = LastName;
    }
}