using Market.Core.InterFaces.MarketInterfaces.IUnitOfWorkS;
using Market.Core.Models.MarketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.InterFaces.MarketInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICartRepository Carts { get; }
        ICategoryRepository Categories { get; }
        ITypeRepository Types { get; }
        ICompanyRepository Companies { get; }
        IProductRepository products { get; }

    }
}
