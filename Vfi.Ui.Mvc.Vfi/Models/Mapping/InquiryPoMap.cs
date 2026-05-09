using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class InquiryPoMap : EntityTypeConfiguration<InquiryPo>
    {
        public InquiryPoMap()
        {
            // Primary Key
            this.HasKey(t => t.InquiryId);

            // Properties
            this.Property(t => t.Unit)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            this.Property(t => t.Currency)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.InquiryNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("InquiryPo", "Purchasing");
            this.Property(t => t.InquiryId).HasColumnName("InquiryId");
            this.Property(t => t.ReferenceId).HasColumnName("ReferenceId");
            this.Property(t => t.ClasstifiedId).HasColumnName("ClasstifiedId");
            this.Property(t => t.PoDetailId).HasColumnName("PoDetailId");
            this.Property(t => t.Unit).HasColumnName("Unit");
            this.Property(t => t.OrderQty).HasColumnName("OrderQty");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.DueDate).HasColumnName("DueDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Currency).HasColumnName("Currency");
            this.Property(t => t.InquiryNumber).HasColumnName("InquiryNumber");
            this.Property(t => t.ManagementSignature).HasColumnName("ManagementSignature");

            // Relationships
            this.HasRequired(t => t.MaterialClassified)
                .WithMany(t => t.InquiryPoes)
                .HasForeignKey(d => d.ClasstifiedId);
            this.HasOptional(t => t.PurchaseOrderDetail)
                .WithMany(t => t.InquiryPoes)
                .HasForeignKey(d => d.PoDetailId);
            this.HasOptional(t => t.Vendor)
                .WithMany(t => t.InquiryPoes)
                .HasForeignKey(d => d.VendorId);

        }
    }
}
