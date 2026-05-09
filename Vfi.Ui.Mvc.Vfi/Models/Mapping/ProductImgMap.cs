using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class ProductImgMap : EntityTypeConfiguration<ProductImg>
    {
        public ProductImgMap()
        {
            this.ToTable("ProductImg", "Factory");
            // Primary Key
            this.HasKey(t => t.ImgId);

            // Properties
            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.Property(t => t.ImgId).HasColumnName("ImgId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ImgUrl).HasColumnName("ImgUrl");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.Step).HasColumnName("Step");
            this.Property(t => t.WarehouseId).HasColumnName("WarehouseId");

            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductImgs)
                .HasForeignKey(d => d.ProductId);
            this.HasRequired(t => t.Warehouse)
                .WithMany(t => t.ProductImgs)
                .HasForeignKey(d => d.WarehouseId);

        }
    }
}
