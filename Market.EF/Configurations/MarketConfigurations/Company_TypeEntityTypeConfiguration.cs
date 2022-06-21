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
    internal class Company_TypeEntityTypeConfiguration : IEntityTypeConfiguration<Company_Type>
    {
        public void Configure(EntityTypeBuilder<Company_Type> builder)
        {
            builder.HasKey(x => new { x.TypeId, x.CompaneyId });

            builder.HasOne(x=> x.Companey)
                .WithMany(x=>x.company_Types)
                .HasForeignKey(x=>x.CompaneyId);
            builder.HasOne(x=>x.Type)
                .WithMany(x=>x.company_Types)
                .HasForeignKey(x=>x.TypeId);


        }
    }
}
