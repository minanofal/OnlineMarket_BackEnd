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
    internal class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.Property(c=> c.Logo).HasDefaultValue(null);


            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(p => p.CategoryId);

            builder.HasMany(c => c.Types)
                .WithOne(t => t.Category)
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(t => t.CategoryId);

            builder.HasMany(c => c.Companies)
                .WithOne(c => c.Category)
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(c => c.CategoryId);
                
        }
    }
}
