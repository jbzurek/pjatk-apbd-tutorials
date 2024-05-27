using Microsoft.EntityFrameworkCore;
using tutorial10.Configuration;

namespace tutorial10.Models;

public class Context : DbContext
{
    public Context()
    {
    }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }


    public virtual DbSet<Medicament> Medicaments { get; set; }
    public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public virtual DbSet<Prescription> Prescriptions { get; set; }
    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DoctorConfig());
        modelBuilder.ApplyConfiguration(new MedicamentConfig());
        modelBuilder.ApplyConfiguration(new PatientConfig());
        modelBuilder.ApplyConfiguration(new PrescriptionConfig());
        modelBuilder.ApplyConfiguration(new PrescriptionMedicamentConfig());
    }
}