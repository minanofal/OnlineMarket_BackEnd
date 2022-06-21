using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces.UnitOfWorkS;
using Market.Core.Models.MarketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.InterFaces.MarketInterfaces
{
    public interface ICategoryRepository
    {
        IUnitOfWorkForEntities UnitOfWork { get; }
        Task<CategoryDisplayDto> CreateCategory(CategoryFormDto dto);
        Task<CategoryDisplayDto> UpdateCategory(CategoryFormDto dto,Guid id);
        Task<IEnumerable< CategoryDisplayDto>> GetCategoriesData(Expression<Func<Category, bool>> match = null);
        Task<DeleteDto> DeleteCategory(Guid id);
        Task<DeleteDto> DeleteCategoryAndTypes(Guid id);
    }
}
