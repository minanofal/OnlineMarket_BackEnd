using Market.Core.AuthenticationsModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.EF.Configurations.AuthCpnfigurations
{
    internal class ApplicationUserEntityTypeConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x=> x.FirstName).HasMaxLength(50);
            builder.Property(x=> x.LastName).HasMaxLength(50);

        }
    }
}
