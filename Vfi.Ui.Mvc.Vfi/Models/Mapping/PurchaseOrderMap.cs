using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class PurchaseOrderMap : EntityTypeConfiguration<PurchaseOrder>
    {
        public PurchaseOrderMap()
        {
            // Primary Key
            this.HasKey(t => t.PurchaseOrderId);

            // Properties
            this.Property(t => t.RevisionNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            this.Property(t => t.EmployeeName)
                .HasMaxLength(50);

            this.Property(t => t.CurrencyCode)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ContractNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PurchaseOrder", "Purchasing");
            this.Property(t => t.PurchaseOrderId).HasColumnName("PurchaseOrderId");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.RevisionNumber).HasColumnName("RevisionNumber");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.OrderDate).HasColumnName("OrderDate");
            this.Property(t => t.ShipDate).HasColumnName("ShipDate");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.EmployeeName).HasColumnName("EmployeeName");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Tolerance).HasColumnName("Tolerance");
            this.Property(t => t.CurrencyCode).HasColumnName("CurrencyCode");
            this.Property(t => t.MaterialClassifiedId).HasColumnName("MaterialClassifiedId");
            this.Property(t => t.ContractNumber).HasColumnName("ContractNumber");
            this.Property(t => t.AddressId).HasColumnName("AddressId");
            this.Property(t => t.BillToId).HasColumnName("BillToId");
            this.Property(t => t.PaymentId).HasColumnName("PaymentId");
            this.Property(t => t.ConditionDeliveryId).HasColumnName("ConditionDeliveryId");
            this.Property(t => t.DeliveryById).HasColumnName("DeliveryById");
            this.Property(t => t.BillOfLanding).HasColumnName("BillOfLanding");

            // Relationships
            this.HasRequired(t => t.MaterialClassified)
                .WithMany(t => t.PurchaseOrders)
                .HasForeignKey(d => d.MaterialClassifiedId);
            this.HasRequired(t => t.Vendor)
                .WithMany(t => t.PurchaseOrders)
                .HasForeignKey(d => d.VendorId);
            this.HasRequired(t => t.DeliveryAddress)
                .WithMany(t => t.PurchaseOrders)
                .HasForeignKey(d => d.AddressId);

        }
    }
}
