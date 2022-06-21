using Market.Core.Models.MarketModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.EF.Configurations.MarketConfigurations
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).HasMaxLength(50);
            builder.Property(x=>x.Description).HasMaxLength(1000);

            builder.HasMany(x => x.productImages)
                .WithOne(x => x.Product)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(X => X.ProductId);
            
        }
    }
}
