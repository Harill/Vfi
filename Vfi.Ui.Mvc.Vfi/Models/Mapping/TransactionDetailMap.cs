using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class TransactionDetailMap : EntityTypeConfiguration<TransactionDetail>
    {
        public TransactionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.TransactionDetailId);

            // Properties
            this.Property(t => t.UnitMeasure)
                .HasMaxLength(3);

            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TransactionDetail", "Inv");
            this.Property(t => t.TransactionDetailId).HasColumnName("TransactionDetailId");
            this.Property(t => t.TransactionId).HasColumnName("TransactionId");
            this.Property(t => t.ReferenceId).HasColumnName("ReferenceId");
            this.Property(t => t.MoP).HasColumnName("MoP");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.QuantityKg).HasColumnName("QuantityKg");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.UnitMeasure).HasColumnName("UnitMeasure");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.ErrorId).HasColumnName("ErrorId");
            this.Property(t => t.ProductInvId).HasColumnName("ProductInvId");
            this.Property(t => t.NextProcessId).HasColumnName("NextProcessId");
            this.Property(t => t.TransactionProductId).HasColumnName("TransactionProductId");
            this.Property(t => t.MachineId).HasColumnName("MachineId");
            this.Property(t => t.PoDetailId).HasColumnName("PoDetailId");
            this.Property(t => t.IsInternal).HasColumnName("IsInternal");
            this.Property(t => t.StoreCode).HasColumnName("StoreCode");
            this.Property(t => t.DefectId).HasColumnName("DefectId");
            this.Property(t => t.DrawerId).HasColumnName("DrawerId");
            this.Property(t => t.NG).HasColumnName("NG");

            // Relationships
            this.HasOptional(t => t.Machine)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.MachineId);
            this.HasOptional(t => t.ProductionDefect)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.DefectId);
            this.HasOptional(t => t.ProductionProcessByMachine)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.NextProcessId);
            this.HasOptional(t => t.InventoryDrawer)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.DrawerId);
            this.HasOptional(t => t.ProcessError)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.ErrorId);
            this.HasOptional(t => t.ProductInventory)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.ProductInvId);
            this.HasOptional(t => t.Transaction)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.TransactionId);
            this.HasOptional(t => t.Material)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.ReferenceId);
            this.HasOptional(t => t.Product)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.ReferenceId);
            this.HasOptional(t => t.TransactionProduct)
                .WithMany(t => t.TransactionDetails)
                .HasForeignKey(d => d.TransactionProductId);

        }
    }
}
