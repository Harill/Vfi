using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class ImportPurchaseOrderDetailMap : EntityTypeConfiguration<ImportPurchaseOrderDetail>
    {
        public ImportPurchaseOrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ImportDetailId);

            // Properties
            this.Property(t => t.StoreCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ImportPurchaseOrderDetail", "Purchasing");
            this.Property(t => t.ImportDetailId).HasColumnName("ImportDetailId");
            this.Property(t => t.ImportId).HasColumnName("ImportId");
            this.Property(t => t.MaterialId).HasColumnName("MaterialId");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.QuantityKg).HasColumnName("QuantityKg");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.UnitWeight).HasColumnName("UnitWeight");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.StoreCode).HasColumnName("StoreCode");
            this.Property(t => t.ProductId).HasColumnName("ProductId");
            this.Property(t => t.PoReferenceDetailId).HasColumnName("PoReferenceDetailId");
            this.Property(t => t.Length).HasColumnName("Length");
            this.Property(t => t.PoDetailId).HasColumnName("PoDetailId");
            this.Property(t => t.NG).HasColumnName("NG");
            this.Property(t => t.Lock).HasColumnName("Lock");

            // Relationships
            this.HasOptional(t => t.Material)
                .WithMany(t => t.ImportPurchaseOrderDetails)
                .HasForeignKey(d => d.MaterialId);
            this.HasRequired(t => t.ImportPurchaseOrder)
                .WithMany(t => t.ImportPurchaseOrderDetails)
                .HasForeignKey(d => d.ImportId);
            this.HasOptional(t => t.PoTaxInvoiceReferenceDetail)
                .WithMany(t => t.ImportPurchaseOrderDetails)
                .HasForeignKey(d => d.PoReferenceDetailId);
            this.HasRequired(t => t.Vendor)
                .WithMany(t => t.ImportPurchaseOrderDetails)
                .HasForeignKey(d => d.VendorId);

        }
    }
}
