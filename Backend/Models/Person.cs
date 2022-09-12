namespace Backend.Models;

public class Person
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public Person(int ID, string Name, string LastName, string Password)
    {
        this.ID = ID;
        this.Name = Name;
        this.LastName = LastName;
        this.Password = Password;
    }

    public bool UpdatePassword(string lastPassword, string newPassword, string confirmPassword)
    {
        bool done = false;
        if (this.ValidatePassword(lastPassword))
        {
            if (newPassword == confirmPassword)
            {
                this.Password = newPassword;
                done = true;
            }
        }
        return done;
    }

    public bool ValidatePassword(string password)
    {
        return this.Password == password;
    }

}