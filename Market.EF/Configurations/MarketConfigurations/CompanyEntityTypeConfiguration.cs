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
    internal class CompanyEntityTypeConfiguration :IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.Property(c=>c.Image).HasDefaultValue(null);

            builder.HasMany(c => c.Products)
                .WithOne(c => c.Company)
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(c => c.CompanyId);

            builder.HasMany(c => c.Types)
                .WithMany(t => t.Companies)
                .UsingEntity<Company_Type>();


        }

    }
}
