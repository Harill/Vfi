using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class TransactionMap : EntityTypeConfiguration<Transaction>
    {
        public TransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionId);

            // Properties
            this.Property(t => t.TransactionCode)
                .HasMaxLength(50);

            this.Property(t => t.EoI)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.CreatedUser)
                .HasMaxLength(50);

            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Transaction", "Inv");
            this.Property(t => t.TransactionId).HasColumnName("TransactionId");
            this.Property(t => t.StockOrderId).HasColumnName("StockOrderId");
            this.Property(t => t.WarehouseIssueId).HasColumnName("WarehouseIssueId");
            this.Property(t => t.WarehouseReceiptId).HasColumnName("WarehouseReceiptId");
            this.Property(t => t.TransactionCode).HasColumnName("TransactionCode");
            this.Property(t => t.EoI).HasColumnName("EoI");
            this.Property(t => t.MoP).HasColumnName("MoP");
            this.Property(t => t.CreatedUser).HasColumnName("CreatedUser");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IsApprove).HasColumnName("IsApprove");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.PoId).HasColumnName("PoId");
            this.Property(t => t.ReferenceId).HasColumnName("ReferenceId");
            this.Property(t => t.IsInternal).HasColumnName("IsInternal");
            this.Property(t => t.IsPacking).HasColumnName("IsPacking");
            this.Property(t => t.FinishDesign).HasColumnName("FinishDesign");

            // Relationships
            this.HasOptional(t => t.StockOrder)
                .WithMany(t => t.Transactions)
                .HasForeignKey(d => d.StockOrderId);
            this.HasOptional(t => t.Warehouse)
                .WithMany(t => t.Transactions)
                .HasForeignKey(d => d.WarehouseIssueId);
            this.HasOptional(t => t.Warehouse1)
                .WithMany(t => t.Transactions1)
                .HasForeignKey(d => d.WarehouseReceiptId);

        }
    }
}
