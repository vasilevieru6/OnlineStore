using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Models.Domain;

namespace OnlineShop.Models.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder
                 .HasOne(x => x.User)
                 .WithOne(x => x.Cart)
                 .HasForeignKey<ShoppingCart>(x => x.UserId);
        }
    }
}
