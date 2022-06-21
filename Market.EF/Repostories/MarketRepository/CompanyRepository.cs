using AutoMapper;
using Market.Core.Consts;
using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces;
using Market.Core.InterFaces.MarketInterfaces.UnitOfWorkS;
using Market.Core.Models.MarketModels;
using Market.EF.Repostories.MarketRepository.UnitOfWorkS;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Market.EF.Repostories.MarketRepository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public  IUnitOfWorkForEntities UnitOfWork { get; private set; }
        private readonly IMapper _mapper;
        public CompanyRepository(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            UnitOfWork = new UnitOfWorkForEntities(_context);
            _mapper = mapper;
        }
     

        public async Task<CompanyDisplayDto> CreateCompany(CompanyFormDto dto)
        {
            var company = new CompanyDisplayDto() { Message=String.Empty};
            if(await UnitOfWork.Companies.GetAsync(c => c.Name == dto.Name) != null)
            {
                company.Message += $"{dto.Name} is already Exist";
            }
            if(await UnitOfWork.Categories.GetAsync(c=>c.Id == dto.CategoryId) == null)
            {
                company.Message +=$" No Category With Id {dto.CategoryId}";
            }
            foreach(var type in dto.Types)
            {

                if(await UnitOfWork.Types.GetAsync(t=>t.Id == type) == null)
                {
                    company.Message +=$" No Type With Id {type}";
                }
                var types = await UnitOfWork.Types.GetAllAsync(t => t.Id == type && t.CategoryId == dto.CategoryId);
                if (types.FirstOrDefault() == null)
                {
                    company.Message +=$" this Type ({type}) In This Category ({dto.CategoryId})";

                }
            }
          
            if (dto.Image != null)
            {
                var images = new List<IFormFile>();
                images.Add(dto.Image);
                if (Images.CheckImageValid(images) != null)
                {
                    company.Message += Images.CheckImageValid(images);
                }
                else
                {
                    company.Image = Images.ImageToByteArray(images).First();
                }
            }
            if(company.Message != String.Empty)
            {
                return company;
            }

            var NewCompany = _mapper.Map<Company>(dto);
            NewCompany.Image = company.Image;
            var data = await UnitOfWork.Companies.CreateAsync(NewCompany);

            foreach(var type in dto.Types.Distinct())
            {
                var Company_types = await UnitOfWork.Company_type.GetAllAsync(ct => ct.TypeId == type && ct.CompaneyId == data.Id);
              
                if (Company_types.FirstOrDefault()==null)
                {
                    await UnitOfWork.Company_type.CreateAsync(new Company_Type { CompaneyId = data.Id , TypeId=type });
                }

            }
            _context.SaveChanges();
            company = _mapper.Map<CompanyDisplayDto>(data);
            company.Types = dto.Types.Distinct().ToList();
            company.Message = null;
            return company;


        }

        public async Task<IEnumerable<CompanyDisplayDto>> GetAllCompanyDisplays(Expression<Func<Company, bool>> match = null)
        {
            if (match == null)
            {
                var companies = await UnitOfWork.Companies.GetAllAsync();
                if (!companies.Any())
                {
                    return null;
                }
                var data = _mapper.Map<List<CompanyDisplayDto>>(companies);
                foreach( var company in data)
                {
                    var ids =await UnitOfWork.Company_type.GetAllAsync(c => c.CompaneyId == company.Id);
                    company.Types = ids.Select(t=>t.TypeId).ToList();
                }
                return data;
            }
            else
            {
                var companies = await UnitOfWork.Companies.GetAllAsync(match);
                if (!companies.Any())
                {
                    return null;
                }

                var data = _mapper.Map<List<CompanyDisplayDto>>(companies);
                foreach (var company in data)
                {
                    var ids = await UnitOfWork.Company_type.GetAllAsync(c => c.CompaneyId == company.Id);
                    company.Types = ids.Select(t => t.TypeId).ToList();
                }
                return data;
                return data;
            }
        }

        public async Task<DeleteDto> DeleteCompanyAndTypes(Guid id)
        {
            var comapny = await UnitOfWork.Companies.GetAsync(c=>c.Id == id);
            if(comapny == null)
            {
                return null;
            }
            var company_types = await UnitOfWork.Company_type.GetAllAsync(c => c.Companey.Id == id);
            if (company_types.Any())
            {
                await UnitOfWork.Company_type.DeleteRange(c => c.CompaneyId == id);

            }
           await UnitOfWork.Companies.DeleteAsync(comapny);
            var result = new DeleteDto() { Id = id , Message="The Company Deleted" };
            return result;
        }

        public async Task<CompanyDisplayDto> UpdateCompany(CompanyFormDto dto, Guid id)
        {
            var company = new CompanyDisplayDto() { Message = String.Empty };
            var oldCompany = await UnitOfWork.Companies.GetAsync(c => c.Id == id);
            if(oldCompany == null)
            {
                company.Message += $"there is no Company with Id{id}";
            }
            if (await UnitOfWork.Companies.GetAsync(c => c.Name == dto.Name && c.Id!=id ) != null)
            {
                company.Message += $"{dto.Name} is already Exist";
            }
            if (await UnitOfWork.Categories.GetAsync(c => c.Id == dto.CategoryId) == null)
            {
                company.Message += $" No Category With Id {dto.CategoryId}";
            }
            foreach (var type in dto.Types)
            {

                if (await UnitOfWork.Types.GetAsync(t => t.Id == type) == null)
                {
                    company.Message += $" No Type With Id {type}";
                }
                var types = await UnitOfWork.Types.GetAllAsync(t => t.Id == type && t.CategoryId == dto.CategoryId);
                if (types.FirstOrDefault() == null)
                {
                    company.Message += $" this Type ({type}) In This Category ({dto.CategoryId})";

                }
            }

            if (dto.Image != null)
            {
                var images = new List<IFormFile>();
                images.Add(dto.Image);
                if (Images.CheckImageValid(images) != null)
                {
                    company.Message += Images.CheckImageValid(images);
                }
                else
                {
                    company.Image = Images.ImageToByteArray(images).First();
                }
            }
            else
            {
                company.Image = oldCompany.Image;
            }
            if (company.Message != String.Empty)
            {
                return company;
            }
            oldCompany.Name = dto.Name;
            oldCompany.Image = company.Image;

            await UnitOfWork.Company_type.DeleteRange(c => c.CompaneyId == id);

            foreach (var type in dto.Types.Distinct())
            {
                var Company_types = await UnitOfWork.Company_type.GetAllAsync(ct => ct.TypeId == type && ct.CompaneyId == id);

                if (Company_types.FirstOrDefault() == null)
                {
                    await UnitOfWork.Company_type.CreateAsync(new Company_Type { CompaneyId = id, TypeId = type });
                }

            }

            await _context.SaveChangesAsync();
            company = _mapper.Map<CompanyDisplayDto>(oldCompany);
            company.Types = dto.Types.Distinct().ToList();
            company.Message = null;
            return company;

        }

    }
}
