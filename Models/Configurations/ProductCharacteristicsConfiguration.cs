using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Models.Domain;

namespace OnlineShop.Models.Configurations
{
    public class ProductCharacteristicsConfiguration : IEntityTypeConfiguration<ProductCharacteristics>
    {
        public void Configure(EntityTypeBuilder<ProductCharacteristics> builder)
        {
            builder.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(30);


        }
    }
}
