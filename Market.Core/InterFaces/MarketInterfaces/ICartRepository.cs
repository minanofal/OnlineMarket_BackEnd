using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces.UnitOfWorkS;
using Market.Core.Models.MarketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.InterFaces.MarketInterfaces
{
    public interface ICartRepository
    {

        IUnitOfWorkForEntities UnitOfWork { get; }
        Task<bool> AddToCart(AddCartDto dto);
        Task<bool> RemoveFromCart(Guid id);
        Task<IEnumerable<CartProductsDto>> GetCartProducts(string UserId);
        
    }
}
