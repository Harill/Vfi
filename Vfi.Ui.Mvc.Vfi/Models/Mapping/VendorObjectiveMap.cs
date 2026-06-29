using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class VendorObjectiveMap : EntityTypeConfiguration<VendorObjective>
    {
        public VendorObjectiveMap()
        {
            this.ToTable("VendorObjective", "Purchasing");
            // Primary Key
            this.HasKey(t => t.ObjectiveId);

            // Properties
            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);
            this.Property(t => t.Note)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.Property(t => t.ObjectiveId).HasColumnName("ObjectiveId");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.Note).HasColumnName("Note");

            this.Property(t => t.PricePoint).HasColumnName("PricePoint");
            this.Property(t => t.LateTimes).HasColumnName("LateTimes");
            this.Property(t => t.TechSpPoint).HasColumnName("TechSpPoint");
            this.Property(t => t.LessTimes).HasColumnName("LessTimes");
            this.Property(t => t.NGTimes).HasColumnName("NGTimes");
            this.Property(t => t.PercentNGNumber).HasColumnName("PercentNGNumber");

            this.Property(t => t.LogisticsPoint).HasColumnName("LogisticsPoint");
            this.Property(t => t.QuantityDeliveryPoint).HasColumnName("QuantityDeliveryPoint");
            this.Property(t => t.NGTimesPoint).HasColumnName("NGTimesPoint");
            this.Property(t => t.NGNumberPoint).HasColumnName("NGNumberPoint");
            this.Property(t => t.TotalPoint).HasColumnName("TotalPoint");

            // Relationships
            this.HasRequired(t => t.Vendor)
                .WithMany(t => t.VendorObjectives)
                .HasForeignKey(d => d.VendorId);


        }
    }
}
