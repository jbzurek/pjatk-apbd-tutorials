namespace tutorial11.Models;

public class Patient
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }


    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}