using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class VendorImgMap : EntityTypeConfiguration<VendorImg>
    {
        public VendorImgMap()
        {
            this.ToTable("VendorImg", "Purchasing");
            // Primary Key
            this.HasKey(t => t.ImgId);

            // Properties
            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.Property(t => t.ImgId).HasColumnName("ImgId");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.ImgUrl).HasColumnName("ImgUrl");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.Type).HasColumnName("Type");


            // Relationships
            this.HasRequired(t => t.Vendor)
                .WithMany(t => t.VendorImgs)
                .HasForeignKey(d => d.VendorId);


        }
    }
}
