using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Models.Domain;
using System;

namespace OnlineShop.Models.Configurations
{
    class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(e => e.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));

            builder.Property(x => x.OrderDate)
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .IsRequired();
        }

    }
}
