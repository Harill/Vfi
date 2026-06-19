using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class ToolInventoryMap : EntityTypeConfiguration<ToolInventory>
    {
        public ToolInventoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ToolInvId);

            // Properties
            this.Property(t => t.UnitMeasure)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ToolInventory", "Inv");
            this.Property(t => t.ToolInvId).HasColumnName("ToolInvId");
            this.Property(t => t.ToolId).HasColumnName("ToolId");
            this.Property(t => t.TotalQuantity).HasColumnName("TotalQuantity");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.UnitMeasure).HasColumnName("UnitMeasure");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ImportDate).HasColumnName("ImportDate");
            this.Property(t => t.ImportQuantity).HasColumnName("ImportQuantity");
            this.Property(t => t.FirstUseDate).HasColumnName("FirstUseDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.StoreCode).HasColumnName("StoreCode");
            this.Property(t => t.NG).HasColumnName("NG");
            this.Property(t => t.Lock).HasColumnName("Lock");

            // Relationships
            this.HasRequired(t => t.Tool)
                .WithMany(t => t.ToolInventories)
                .HasForeignKey(d => d.ToolId);
            this.HasRequired(t => t.Vendor)
                .WithMany(t => t.ToolInventories)
                .HasForeignKey(d => d.VendorId);

        }
    }
}
