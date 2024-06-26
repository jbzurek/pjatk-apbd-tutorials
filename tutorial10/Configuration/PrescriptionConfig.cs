using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tutorial10.Models;

namespace tutorial10.Configuration;

public class PrescriptionConfig : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(e => e.IdPrescription);

        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.DueDate).IsRequired();

        builder
            .HasOne(e => e.IdPatient)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(e => e.IdPatient)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Prescription_Patient");

        builder
            .HasOne(e => e.IdDoctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(e => e.IdDoctor)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Prescription_Doctor");
    }
}
