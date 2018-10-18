using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Models.Domain;

namespace OnlineShop.Models.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Category)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.SubCategory)
                .HasMaxLength(50);
            
        }
    }
}
