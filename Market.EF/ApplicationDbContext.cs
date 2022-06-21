using Market.Core.AuthenticationsModels;
using Market.Core.Models.MarketModels;
using Market.EF.Configurations.AuthCpnfigurations;
using Market.EF.Configurations.MarketConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Type = Market.Core.Models.MarketModels.Type;

namespace Market.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "0884256f-48ef-41ae-9c6f-50b663959cbd", Name = "User", NormalizedName = "User".ToUpper(), ConcurrencyStamp = "19418a68-0b22-4b1e-b404-7e689632d784" },
                new IdentityRole { Id = "4b04b503-e4af-47bb-80bb-fdd772271c38", Name = "Admin", NormalizedName = "Admin".ToUpper(), ConcurrencyStamp = "b22fff30-23d3-4abc-9d3e-9375ea784352" }
                );
          
  
            
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId= "4b04b503-e4af-47bb-80bb-fdd772271c38" ,UserId= "f683ab07-2020-4371-8839-07edc7e9d77a" }
            );

            builder.Entity<ApplicationUser>().ToTable("Users", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");


            builder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.ProviderKey, x.LoginProvider });
            builder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.RoleId, x.UserId });
            builder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.LoginProvider, x.Name, x.UserId });

            new ApplicationUserEntityTypeConfigurations().Configure(builder.Entity<ApplicationUser>());

            /************************************************/

            new CategoryEntityTypeConfiguration().Configure(builder.Entity<Category>());
            new CompanyEntityTypeConfiguration().Configure(builder.Entity<Company>());
            new TypeEntityTypeConfiguration().Configure(builder.Entity<Type>());
            new ProductEntityTypeConfiguration().Configure(builder.Entity<Product>());
            new ProductImageEntityTypeConfiguration().Configure(builder.Entity<ProductImage>());
            new ClotheEntityTypeConfiguration().Configure(builder.Entity<Clothe>());
            new Clothe_Color_Size_countEntityTypeConfiguration().Configure(builder.Entity<Clothe_Color_Size_count>());
            new Company_TypeEntityTypeConfiguration().Configure(builder.Entity<Company_Type>());
            new CartEntityTypeConfiguration().Configure(builder.Entity<Cart>());

        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Company> companies { get; set; }
        public DbSet<Type> types { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductImage> productImages { get; set; }
        public DbSet<Clothe> clothes { get; set; }
        public DbSet<Clothe_Color_Size_count> Clothe_Color_Size_Counts { get; set; }
        public DbSet<Company_Type> companies_Types { get; set; }
        public DbSet<Cart> carts { get; set; }


    }
}
