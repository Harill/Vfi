using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class VendorMap : EntityTypeConfiguration<Vendor>
    {
        public VendorMap()
        {
            // Primary Key
            this.HasKey(t => t.VendorId);

            // Properties
            this.Property(t => t.VendorCode)
                .HasMaxLength(50);

            this.Property(t => t.VendorName)
                .HasMaxLength(255);

            this.Property(t => t.ShortName)
                .HasMaxLength(255);

            this.Property(t => t.CompanyName)
                .HasMaxLength(255);

            this.Property(t => t.ContactName)
                .HasMaxLength(255);

            this.Property(t => t.Address)
                .HasMaxLength(255);

            this.Property(t => t.Eaddress)
                .HasMaxLength(100);

            this.Property(t => t.Phone)
                .HasMaxLength(100);

            this.Property(t => t.Fax)
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.TaxCode)
                .HasMaxLength(50);

            this.Property(t => t.BankAccount)
                .HasMaxLength(50);

            this.Property(t => t.SpecialInfo)
                .HasMaxLength(255);

            this.Property(t => t.Note)
                .HasMaxLength(500);

            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Vendor", "Purchasing");
            this.Property(t => t.VendorId).HasColumnName("VendorId");
            this.Property(t => t.VendorCode).HasColumnName("VendorCode");
            this.Property(t => t.VendorName).HasColumnName("VendorName");
            this.Property(t => t.ShortName).HasColumnName("ShortName");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Eaddress).HasColumnName("Eaddress");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.TaxCode).HasColumnName("TaxCode");
            this.Property(t => t.BankAccount).HasColumnName("BankAccount");
            this.Property(t => t.MaxCredit).HasColumnName("MaxCredit");
            this.Property(t => t.SpecialInfo).HasColumnName("SpecialInfo");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.MaterialClassifiedId).HasColumnName("MaterialClassifiedId");

            // Relationships
            this.HasOptional(t => t.MaterialClassified)
                .WithMany(t => t.Vendors)
                .HasForeignKey(d => d.MaterialClassifiedId);

        }
    }
}
