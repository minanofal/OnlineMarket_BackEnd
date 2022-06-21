using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces.UnitOfWorkS;
using Market.Core.Models.MarketModels;

namespace Market.Core.InterFaces.MarketInterfaces
{
    public interface ITypeRepository
    {
  
        IUnitOfWorkForEntities UnitOfWork { get; }
        Task<TypeDisplayDto> CreateType(TypeFormeDto dto);
        Task<IEnumerable<TypeDisplayDto>> GetTypes(Expression<Func<Models.MarketModels.Type, bool>> match = null);
        Task<Guid> DeleteType(Guid id);
        Task<TypeDisplayDto> UpdateType(Guid id, TypeFormeDto dto);
    }
}