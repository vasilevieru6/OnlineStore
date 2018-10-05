using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Models.Domain;

namespace OnlineShop.Models.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.Property(x => x.Country)
                .HasMaxLength(50);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.PostalCode)
                .HasMaxLength(15);
        }
    }
}
