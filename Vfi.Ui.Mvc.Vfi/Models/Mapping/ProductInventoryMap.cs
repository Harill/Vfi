using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class ProductInventoryMap : EntityTypeConfiguration<ProductInventory>
    {
        public ProductInventoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductInventoryId);

            // Properties
            this.Property(t => t.UnitMeasure)
                .HasMaxLength(3);

            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProductInventory", "Inv");
            this.Property(t => t.ProductInventoryId).HasColumnName("ProductInventoryId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.WarehouseId).HasColumnName("WarehouseId");
            this.Property(t => t.TotalQty).HasColumnName("TotalQty");
            this.Property(t => t.AvailableQty).HasColumnName("AvailableQty");
            this.Property(t => t.UnavailableQty).HasColumnName("UnavailableQty");
            this.Property(t => t.UnitMeasure).HasColumnName("UnitMeasure");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.ImportDate).HasColumnName("ImportDate");
            this.Property(t => t.ExportDate).HasColumnName("ExportDate");
            this.Property(t => t.ErrorId).HasColumnName("ErrorId");
            this.Property(t => t.MachineId).HasColumnName("MachineId");
            this.Property(t => t.MaterialInvId).HasColumnName("MaterialInvId");
            this.Property(t => t.ByProcessMachineId).HasColumnName("ByProcessMachineId");
            this.Property(t => t.StoreCode).HasColumnName("StoreCode");
            this.Property(t => t.DefectId).HasColumnName("DefectId");
            this.Property(t => t.NG).HasColumnName("NG");

            // Relationships
            this.HasOptional(t => t.Machine)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.MachineId);
            this.HasOptional(t => t.ProductionDefect)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.DefectId);
            this.HasOptional(t => t.MaterialInventory)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.MaterialInvId);
            this.HasOptional(t => t.ProcessError)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.ErrorId);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.ProductId);
            this.HasRequired(t => t.Warehouse)
                .WithMany(t => t.ProductInventories)
                .HasForeignKey(d => d.WarehouseId);

        }
    }
}
