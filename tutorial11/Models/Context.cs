using Microsoft.EntityFrameworkCore;
using tutorial11.Configurations;

namespace tutorial11.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DoctorConfig());
        modelBuilder.ApplyConfiguration(new MedicamentConfig());
        modelBuilder.ApplyConfiguration(new PatientConfig());
        modelBuilder.ApplyConfiguration(new PrescriptionConfig());
        modelBuilder.ApplyConfiguration(new PrescriptionMedicamentConfig());
    }
}