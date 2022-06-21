using Microsoft.EntityFrameworkCore;
using Market.Core.Models.MarketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Market.Core.Models.MarketModels.Type;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.EF.Configurations.MarketConfigurations
{
    internal class TypeEntityTypeConfiguration : IEntityTypeConfiguration<Type>
    {
        public void Configure(EntityTypeBuilder<Type> builder)
        {
            builder.HasKey(t=>t.Id);
            builder.Property(t => t.Name).HasMaxLength(50);

            builder.HasMany(t => t.Products)
                .WithOne(p => p.Type)
                .HasPrincipalKey(t => t.Id)
                .HasForeignKey(p => p.TypeId);
     
        }
    }
}
