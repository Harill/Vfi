using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class TransactionImgMap : EntityTypeConfiguration<TransactionImg>
    {
        public TransactionImgMap()
        {
            this.ToTable("TransactionImg", "Inv");
            // Primary Key
            this.HasKey(t => t.ImgId);

            // Properties
            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);
            this.Property(t => t.TransactionNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.Property(t => t.ImgId).HasColumnName("ImgId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ImgUrl).HasColumnName("ImgUrl");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.TransactionNumber).HasColumnName("TransactionNumber");
            this.Property(t => t.Name).HasColumnName("Name");
            //this.Property(t => t.TransactionId).HasColumnName("TransactionId");

            // Relationships
            this.HasRequired(t => t.Transaction)      // Bảng TransactionImg bắt buộc phải có Transaction
                .WithMany(t => t.TransactionImg)      // Một Transaction có thể có nhiều TransactionImg
                .HasForeignKey(d => d.TransactionId); // Khóa ngoại là TransactionId
        }
    }
}
