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
    internal class ClotheEntityTypeConfiguration :   ProductEntityTypeConfiguration ,IEntityTypeConfiguration<Clothe> 
    {
        public void Configure(EntityTypeBuilder<Clothe> builder)
        {
           
            builder.Property(c => c.Genre).HasMaxLength(10);

            builder.HasMany(c=>c.clothe_Color_Size_Counts)
                .WithOne(c=>c.Clothe)
                .HasPrincipalKey(c => c.Id)
                .HasForeignKey(c => c.ClotheId);

      
               
            
        }
    }
}
