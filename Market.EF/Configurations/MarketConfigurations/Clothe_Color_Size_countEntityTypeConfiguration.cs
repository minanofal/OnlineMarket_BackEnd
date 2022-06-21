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
    internal class Clothe_Color_Size_countEntityTypeConfiguration : IEntityTypeConfiguration<Clothe_Color_Size_count>
    {
        public void Configure(EntityTypeBuilder<Clothe_Color_Size_count> builder)
        {
            builder.HasKey(c => new { c.ClotheId, c.Color, c.Size });
            builder.Property(c => c.Color).HasMaxLength(50);
            builder.Property(c => c.Size).HasMaxLength(10);
        }
    }
}
