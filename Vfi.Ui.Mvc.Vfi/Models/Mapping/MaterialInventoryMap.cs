using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class MaterialInventoryMap : EntityTypeConfiguration<MaterialInventory>
    {
        public MaterialInventoryMap()
        {
            // Primary Key
            this.HasKey(t => t.MaterialInventoryId);

            // Properties
            this.Property(t => t.UnitMeasure)
                .HasMaxLength(3);

            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            this.Property(t => t.LotNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MaterialInventory", "Inv");
            this.Property(t => t.MaterialInventoryId).HasColumnName("MaterialInventoryId");
            this.Property(t => t.MaterialId).HasColumnName("MaterialId");
            this.Property(t => t.TotalQty).HasColumnName("TotalQty");
            this.Property(t => t.TotalQtyKg).HasColumnName("TotalQtyKg");
            this.Property(t => t.AvailableQty).HasColumnName("AvailableQty");
            this.Property(t => t.UnavailableQty).HasColumnName("UnavailableQty");
            this.Property(t => t.UnitMeasure).HasColumnName("UnitMeasure");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.UnitWeight).HasColumnName("UnitWeight");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.ImportDate).HasColumnName("ImportDate");
            this.Property(t => t.ImportQuantity).HasColumnName("ImportQuantity");
            this.Property(t => t.ImportQuantityKg).HasColumnName("ImportQuantityKg");
            this.Property(t => t.FirstUseDate).HasColumnName("FirstUseDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.StoreCode).HasColumnName("StoreCode");
            this.Property(t => t.Length).HasColumnName("Length");
            this.Property(t => t.InfoImg).HasColumnName("InfoImg");
            this.Property(t => t.InfoImg2).HasColumnName("InfoImg2");
            this.Property(t => t.NG).HasColumnName("NG");
            this.Property(t => t.Lock).HasColumnName("Lock");

            // Relationships
            this.HasRequired(t => t.Material)
                .WithMany(t => t.MaterialInventories)
                .HasForeignKey(d => d.MaterialId);
            this.HasOptional(t => t.Vendor)
                .WithMany(t => t.MaterialInventories)
                .HasForeignKey(d => d.VendorId);

        }
    }
}
