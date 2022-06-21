using AutoMapper;
using Market.Core.InterFaces.MarketInterfaces;
using Market.Core.InterFaces.MarketInterfaces.UnitOfWorkS;
using Market.Core.Models.MarketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = Market.Core.Models.MarketModels.Type;

namespace Market.EF.Repostories.MarketRepository.UnitOfWorkS
{
    public class UnitOfWorkForEntities : IUnitOfWorkForEntities
    {
        public IBaseRepository<Cart> Carts { get; private set; }
        public IBaseRepository<Category> Categories { get; private set; }
        public IBaseRepository<Type> Types { get; private set; }
        public IBaseRepository<Company> Companies { get; private set; }
        public IBaseRepository<Company_Type> Company_type { get; private set; }
        public IBaseRepository<Product> Products { get; private set; }
        public IBaseRepository<ProductImage> ProductImages { get; private set; }

        public UnitOfWorkForEntities(ApplicationDbContext context )
        {
            Categories = new BaseRepository<Category>(context);
            Carts = new BaseRepository<Cart>(context);
            Types = new BaseRepository<Type>(context);
            Company_type = new BaseRepository<Company_Type>(context);
            Companies = new BaseRepository<Company>(context);
            Products = new BaseRepository<Product>(context);
            ProductImages = new BaseRepository<ProductImage>(context);
        }

   
    }
}
