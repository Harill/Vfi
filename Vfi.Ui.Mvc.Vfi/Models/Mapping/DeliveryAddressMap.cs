using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Vfi.Ui.Mvc.Vfi.Models.Mapping
{
    public class DeliveryAddressMap : EntityTypeConfiguration<DeliveryAddress>
    {
        public DeliveryAddressMap()
        {
            // Primary Key
            this.HasKey(t => t.AddressId);

            // Properties
            this.Property(t => t.AddressName)
                .HasMaxLength(50);
            this.Property(t => t.ModifiedUser)
                .HasMaxLength(50);
            this.Property(t => t.Telephone)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("DeliveryAddress", "Purchasing");
            this.Property(t => t.AddressId).HasColumnName("AddressId");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.AddressName).HasColumnName("AddressName");
            this.Property(t => t.AddressShortName).HasColumnName("AddressShortName");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.ModifiedUser).HasColumnName("ModifiedUser");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.Telephone).HasColumnName("Telephone");
            this.Property(t => t.Recipient).HasColumnName("Recipient");

            // Relationships

        }
    }
}
