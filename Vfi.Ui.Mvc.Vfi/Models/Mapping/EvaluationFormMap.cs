using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class EvaluationFormMap : EntityTypeConfiguration<EvaluationForm>
    {
        public EvaluationFormMap()
        {
            // Primary Key
            this.HasKey(t => t.FormId);

            // Properties

            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EvaluationForm", "Purchasing");

            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.FormId).HasColumnName("FormId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.MaterialClassifiedId).HasColumnName("MaterialClassifiedId");

            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.PricePoint).HasColumnName("PricePoint");
            this.Property(t => t.TechSpPoint).HasColumnName("TechSpPoint");
            this.Property(t => t.LogisticsPoint).HasColumnName("LogisticsPoint");
            this.Property(t => t.QuantityDeliveryPoint).HasColumnName("QuantityDeliveryPoint");
            this.Property(t => t.NGTimePoint).HasColumnName("NGTimePoint");
            this.Property(t => t.NGNumberPoint).HasColumnName("NGNumberPoint");
            this.Property(t => t.ApprovedUser).HasColumnName("ApprovedUser");
            this.Property(t => t.ApprovedDate).HasColumnName("ApprovedDate");
            this.Property(t => t.ZeroPointCount).HasColumnName("ZeroPointCount");
            this.Property(t => t.TotalPoint).HasColumnName("TotalPoint");
            this.Property(t => t.Grade).HasColumnName("Grade");
            this.Property(t => t.NGQuantity).HasColumnName("NGQuantity");
            this.Property(t => t.OrderQuantity).HasColumnName("OrderQuantity");


            // Relationships
            this.HasRequired(t => t.Vendor)
                .WithMany(t => t.EvaluationForms)
                .HasForeignKey(d => d.VendorId);

            this.HasRequired(t => t.MaterialClassified)
                .WithMany(t => t.EvaluationForms)
                .HasForeignKey(d => d.MaterialClassifiedId);

        }
    }
}
