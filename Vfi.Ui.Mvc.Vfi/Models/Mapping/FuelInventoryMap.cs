using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class FuelInventoryMap : EntityTypeConfiguration<FuelInventory>
    {
        public FuelInventoryMap()
        {
            // Primary Key
            this.HasKey(t => t.FuelInvId);

            // Properties
            this.Property(t => t.UnitMeasure)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("FuelInventory", "Inv");
            this.Property(t => t.FuelInvId).HasColumnName("FuelInvId");
            this.Property(t => t.FuelId).HasColumnName("FuelId");
            this.Property(t => t.TotalQuantity).HasColumnName("TotalQuantity");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.UnitMeasure).HasColumnName("UnitMeasure");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.ImportDate).HasColumnName("ImportDate");
            this.Property(t => t.NG).HasColumnName("NG");
            this.Property(t => t.Lock).HasColumnName("Lock");

            // Relationships
            this.HasRequired(t => t.Fuel)
                .WithMany(t => t.FuelInventories)
                .HasForeignKey(d => d.FuelId);
            this.HasRequired(t => t.Vendor)
                .WithMany(t => t.FuelInventories)
                .HasForeignKey(d => d.VendorId);

        }
    }
}
