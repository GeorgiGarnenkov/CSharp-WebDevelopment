using System;
using Eventures.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventures.Data.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);

            builder
                .HasOne(x => x.Event)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.EventId);

            builder
                .Property(x => x.OrderedOn)
                .HasDefaultValue(DateTime.UtcNow);
        }
    }
}
