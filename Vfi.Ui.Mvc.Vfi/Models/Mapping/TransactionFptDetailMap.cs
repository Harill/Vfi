using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class TransactionFptDetailMap : EntityTypeConfiguration<TransactionFptDetail>
    {
        public TransactionFptDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.DetailId);

            // Properties
            this.Property(t => t.UnitMeasure)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TransactionFptDetail", "Purchasing");
            this.Property(t => t.DetailId).HasColumnName("DetailId");
            this.Property(t => t.TransactionId).HasColumnName("TransactionId");
            this.Property(t => t.FptId).HasColumnName("FptId");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.MachineId).HasColumnName("MachineId");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.UnitMeasure).HasColumnName("UnitMeasure");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.PoReferenceDetailId).HasColumnName("PoReferenceDetailId");
            this.Property(t => t.PoDetailId).HasColumnName("PoDetailId");
            this.Property(t => t.IsInternal).HasColumnName("IsInternal");
            this.Property(t => t.NG).HasColumnName("NG");
            this.Property(t => t.Lock).HasColumnName("Lock");


            // Relationships
            this.HasOptional(t => t.PoTaxInvoiceReferenceDetail)
                .WithMany(t => t.TransactionFptDetails)
                .HasForeignKey(d => d.PoReferenceDetailId);
            this.HasRequired(t => t.TransactionFpt)
                .WithMany(t => t.TransactionFptDetails)
                .HasForeignKey(d => d.TransactionId);

        }
    }
}
