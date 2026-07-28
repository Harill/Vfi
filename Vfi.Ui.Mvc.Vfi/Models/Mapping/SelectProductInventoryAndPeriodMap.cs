using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class SelectProductInventoryAndPeriodMap : EntityTypeConfiguration<SelectProductInventoryAndPeriod>
    {
        public SelectProductInventoryAndPeriodMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ProductId, t.WarehouseId, t.CustomerId });

            // Properties
            this.Property(t => t.ProductId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProductCode)
                .HasMaxLength(50);

            this.Property(t => t.WarehouseId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WarehouseName)
                .HasMaxLength(50);

            this.Property(t => t.CustomerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CustomerCode)
                .HasMaxLength(50);

            this.Property(t => t.LotNumber)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SelectProductInventoryAndPeriod");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ProductCode).HasColumnName("ProductCode");
            this.Property(t => t.WarehouseId).HasColumnName("WarehouseId");
            this.Property(t => t.WarehouseName).HasColumnName("WarehouseName");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.CustomerCode).HasColumnName("CustomerCode");
            this.Property(t => t.TotalInv).HasColumnName("TotalInv");
            this.Property(t => t.TotalPeriod).HasColumnName("TotalPeriod");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.ProductInventoryId).HasColumnName("ProductInventoryId");
        }
    }
}
