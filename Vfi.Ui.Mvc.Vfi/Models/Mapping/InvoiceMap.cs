using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class InvoiceMap : EntityTypeConfiguration<Invoice>
    {
        public InvoiceMap()
        {
            // Primary Key
            this.HasKey(t => t.InvoiceId);

            // Properties
            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            this.Property(t => t.InvoiceNumber)
                .HasMaxLength(50);

            this.Property(t => t.TaxInvoice)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Invoice", "Sales");
            this.Property(t => t.InvoiceId).HasColumnName("InvoiceId");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.PriceListId).HasColumnName("PriceListId");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ExportId).HasColumnName("ExportId");
            this.Property(t => t.InvoiceNumber).HasColumnName("InvoiceNumber");
            this.Property(t => t.OrderId).HasColumnName("OrderId");
            this.Property(t => t.TaxInvoice).HasColumnName("TaxInvoice");
            this.Property(t => t.TaxPercent).HasColumnName("TaxPercent");
            this.Property(t => t.ShipmentDate).HasColumnName("ShipmentDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.ExchangeRate).HasColumnName("ExchangeRate");
            this.Property(t => t.FinishDesign).HasColumnName("FinishDesign");

            // Relationships
            this.HasOptional(t => t.ExportFormTP_KD)
                .WithMany(t => t.Invoices)
                .HasForeignKey(d => d.ExportId);
            this.HasRequired(t => t.Customer)
                .WithMany(t => t.Invoices)
                .HasForeignKey(d => d.CustomerId);

        }
    }
}
