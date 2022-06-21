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
    public interface ICompanyRepository
    {
        IUnitOfWorkForEntities UnitOfWork { get; }
        Task<CompanyDisplayDto> CreateCompany(CompanyFormDto dto);
        Task<IEnumerable<CompanyDisplayDto>> GetAllCompanyDisplays(Expression<Func<Company, bool>> match = null);
        Task<DeleteDto> DeleteCompanyAndTypes(Guid id);
        Task<CompanyDisplayDto> UpdateCompany(CompanyFormDto dto , Guid id);

    }
}
