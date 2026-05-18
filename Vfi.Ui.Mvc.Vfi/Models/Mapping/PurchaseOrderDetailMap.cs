using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class PurchaseOrderDetailMap : EntityTypeConfiguration<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.PurchaseOrderDetailId);

            // Properties
            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            this.Property(t => t.Unit)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("PurchaseOrderDetail", "Purchasing");
            this.Property(t => t.PurchaseOrderDetailId).HasColumnName("PurchaseOrderDetailId");
            this.Property(t => t.PurchaseOrderId).HasColumnName("PurchaseOrderId");
            this.Property(t => t.MaterialClassifiedId).HasColumnName("MaterialClassifiedId");
            this.Property(t => t.ReferenceId).HasColumnName("ReferenceId");
            this.Property(t => t.OrderQty).HasColumnName("OrderQty");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.ReceivedQty).HasColumnName("ReceivedQty");
            this.Property(t => t.RejectedQty).HasColumnName("RejectedQty");
            this.Property(t => t.DueDate).HasColumnName("DueDate");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.IsComplete).HasColumnName("IsComplete");
            this.Property(t => t.Unit).HasColumnName("Unit");
            this.Property(t => t.Met).HasColumnName("Met");
            this.Property(t => t.Standard).HasColumnName("Standard");


            // Relationships
            this.HasRequired(t => t.MaterialClassified)
                .WithMany(t => t.PurchaseOrderDetails)
                .HasForeignKey(d => d.MaterialClassifiedId);
            this.HasRequired(t => t.PurchaseOrder)
                .WithMany(t => t.PurchaseOrderDetails)
                .HasForeignKey(d => d.PurchaseOrderId);

        }
    }
}
