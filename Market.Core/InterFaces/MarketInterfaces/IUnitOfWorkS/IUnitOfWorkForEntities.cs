using Market.Core.Models.MarketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Market.Core.InterFaces.MarketInterfaces.UnitOfWorkS
{
    public interface IUnitOfWorkForEntities 
    {
        IBaseRepository<Cart> Carts { get; }
        IBaseRepository<Category> Categories { get; }
        IBaseRepository<Models.MarketModels.Type> Types { get; }
        IBaseRepository<Company> Companies { get; }
        IBaseRepository<Company_Type> Company_type { get; }
        IBaseRepository<Product> Products { get; }
        IBaseRepository<ProductImage> ProductImages { get; }
    }
}
