using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class MaterialImgMap : EntityTypeConfiguration<MaterialImg>
    {
        public MaterialImgMap()
        {
            this.ToTable("MaterialImg", "Production");
            // Primary Key
            this.HasKey(t => t.ImgId);

            // Properties
            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);
            //this.Property(t => t.TransactionNumber)
            //    .HasMaxLength(50);

            // Table & Column Mappings
            this.Property(t => t.ImgId).HasColumnName("ImgId");
            this.Property(t => t.MaterialId).HasColumnName("MaterialId");
            this.Property(t => t.ImgUrl).HasColumnName("ImgUrl");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            //this.Property(t => t.TransactionNumber).HasColumnName("TransactionNumber");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Name).HasColumnName("Name");
            


            // Relationships
            this.HasRequired(t => t.Material)      // Bảng TransactionImg bắt buộc phải có Transaction
                .WithMany(t => t.MaterialImgs)      // Một Transaction có thể có nhiều TransactionImg
                .HasForeignKey(d => d.MaterialId); // Khóa ngoại là TransactionId
        }
    }
}
