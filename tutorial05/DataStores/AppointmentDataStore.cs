using tutorial05.Models;

namespace tutorial05.DataStores;

public class AppointmentDataStore
{
    public List<Appointment> Appointments { get; set; }

    public static AppointmentDataStore Current { get; } = new AppointmentDataStore();

    public AppointmentDataStore()
    {
        Appointments = new List<Appointment>();
    }
    
}