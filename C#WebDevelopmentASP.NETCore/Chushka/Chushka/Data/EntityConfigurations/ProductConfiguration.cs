using System;
using Chushka.Data.Models;
using Chushka.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chushka.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Price)
                .HasColumnType("DECIMAL(15, 2)");

            builder
                .Property(x => x.Type)
                .HasConversion(
                    x => x.ToString(),
                    x => (ProductType) Enum.Parse(typeof(ProductType), x));
        }
    }
}
