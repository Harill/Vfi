using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class QcImgMap : EntityTypeConfiguration<QcImg>
    {
        public QcImgMap()
        {
            this.ToTable("QcImg", "Factory");
            // Primary Key
            this.HasKey(t => t.ImgId);

            // Properties
            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.Property(t => t.ImgId).HasColumnName("ImgId");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.ImgUrl).HasColumnName("ImgUrl");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Type).HasColumnName("Type");


            // Relationships
            this.HasRequired(t => t.Product)
                .WithMany(t => t.QcImgs)
                .HasForeignKey(d => d.ProductId);


        }
    }
}
