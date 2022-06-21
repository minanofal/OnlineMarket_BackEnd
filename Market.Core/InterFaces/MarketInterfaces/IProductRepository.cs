using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces.UnitOfWorkS;
using Market.Core.Models.MarketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.InterFaces.MarketInterfaces.IUnitOfWorkS
{
    public interface IProductRepository
    {
        IUnitOfWorkForEntities UnitOfWork { get; }
        Task<ProductDisplayDto> CreateProduct(ProductFormDto dto);
        Task<IEnumerable<ProductDisplayDto>> GetProducts(Expression<Func<Product , bool>> match = null);
        Task<DeleteDto> DeleteProduct(Guid id);
        Task<ProductDisplayDto> UpdateProduct(ProductUpdateDto dto , Guid id);
    }
}
