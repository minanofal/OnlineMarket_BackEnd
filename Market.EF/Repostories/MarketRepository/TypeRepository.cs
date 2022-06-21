using AutoMapper;
using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces;
using Market.Core.InterFaces.MarketInterfaces.UnitOfWorkS;
using Market.Core.Models.MarketModels;
using Market.EF.Repostories.MarketRepository.UnitOfWorkS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Type = Market.Core.Models.MarketModels.Type;

namespace Market.EF.Repostories.MarketRepository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly ApplicationDbContext _context;
      
        public IUnitOfWorkForEntities UnitOfWork { get; private set; }
        private readonly IMapper _mapper;
        public TypeRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            //Types = new BaseRepository<Type>(context);
            //Categories = new CategoryRepository(context,mapper);
            UnitOfWork = new UnitOfWorkForEntities(_context);
        }


        public async Task<TypeDisplayDto> CreateType(TypeFormeDto dto)
        {
            var type = new TypeDisplayDto() { Message = String.Empty };
            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == dto.CategoryId);

            if (category == null)
            {
                type.Message += $"No Category With ID {dto.CategoryId} , ";
            }
            if(await UnitOfWork.Types.GetAsync(t=>t.Name == dto.Name) != null)
            {
                type.Message += $"{dto.Name} is Already Exist";
               
            }
            if(type.Message != String.Empty)
            {
                return type;
            }
            var data = _mapper.Map<Type>(dto);
            var result = await UnitOfWork.Types.CreateAsync(data);
            
            type = _mapper.Map<TypeDisplayDto>(result);
            type.Message = null;
            return type;
        }

        public async Task<IEnumerable<TypeDisplayDto>> GetTypes(Expression<Func<Type, bool>> match = null)
        {
            if (match == null)
            {
                var types = await UnitOfWork.Types.GetAllAsync();
                var data = _mapper.Map<IEnumerable<TypeDisplayDto>>(types);
                return data;
            }
            else
            {
                var types = await UnitOfWork.Types.GetAllAsync(match);
                if (!types.Any())
                {

                    return null;
                }
                var data = _mapper.Map<IEnumerable<TypeDisplayDto>>(types);
                return data;
            }
        }

        public async Task<Guid> DeleteType(Guid id)
        {
            var type = await UnitOfWork.Types.GetAllAsync(t => t.Id == id);
            if(type.FirstOrDefault() == null)
            {
                return Guid.Empty;
            }
            var company_types = await UnitOfWork.Company_type.GetAllAsync(c => c.TypeId == id);
            if (company_types.Any())
            {
               await UnitOfWork.Company_type.DeleteRange(c => c.TypeId == id);
                foreach (var companyType in company_types)
                {
                    var company = await UnitOfWork.Company_type.GetAllAsync(c => c.CompaneyId == companyType.CompaneyId);
                    if (!company.Any())
                    {
                        var c = await UnitOfWork.Companies.GetAsync(c => c.Id == companyType.CompaneyId);
                       await UnitOfWork.Companies.DeleteAsync(c);
                    }
                }
                   

            }

           await UnitOfWork.Types.DeleteAsync(type.First());
            return id;
        }

        public async Task<TypeDisplayDto> UpdateType(Guid id, TypeFormeDto dto)
        {
            var type = await UnitOfWork.Types.GetAsync(t => t.Id == id);
            var result = new TypeDisplayDto() { Message = String.Empty };
            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == dto.CategoryId);


            if (type == null)
            {
                result.Message +=$"there is no type with Id {id}";
            }

            if (category == null)
            {
                result.Message += $"No Category With ID {dto.CategoryId} , ";
            }
            if (await UnitOfWork.Types.GetAsync(t => t.Name == dto.Name && t.Id != id) != null)
            {
                result.Message += $"{dto.Name} is Already Exist";

            }
            if (result.Message != String.Empty)
            {
                return result;
            }


            type.Name= dto.Name;
            type.CategoryId= dto.CategoryId;

            _context.SaveChanges();

            result = _mapper.Map<TypeDisplayDto>(type);
            result.Message = null;
            return result;

        }
    }
}
