using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(o => o.Items)
                 .WithOne()
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.SubTotal).HasColumnType("decimal(8,2)");

            builder.OwnsOne(o => o.ShipToAddress, address =>
            {
                address.Property(x => x.FirstName).HasMaxLength(50);
                address.Property(x => x.LastName).HasMaxLength(50);
                address.Property(x => x.City).HasMaxLength(50);
                address.Property(x => x.Country).HasMaxLength(50);
                address.Property(x => x.Street).HasMaxLength(50);


            });
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(50);
        }
    }
}
