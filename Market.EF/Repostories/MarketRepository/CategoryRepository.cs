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
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        //public IBaseRepository<Category> Categories { get; private set; }
        public IUnitOfWorkForEntities UnitOfWork { get; private set; }
        public CategoryRepository(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            //Categories = new BaseRepository<Category>(_context);
            UnitOfWork = new UnitOfWorkForEntities(_context );
        }

        public async Task<CategoryDisplayDto> CreateCategory(CategoryFormDto dto)
        {
            var category = new CategoryDisplayDto() { Message= String.Empty };
            var categories = await UnitOfWork.Categories.GetAllAsync(c => c.Name == dto.Name);
            if (categories.FirstOrDefault() != null)
            {
                category.Message += $"{dto.Name} is already exist";
               
            }
            if(dto.Logo != null)
            {
                var images = new List<IFormFile>();
                images.Add(dto.Logo);
               if( Images.CheckImageValid(images) != null)
                {
                    category.Message += Images.CheckImageValid(images);
                }
                else
                {
                  category.Logo= Images.ImageToByteArray(images).First();
                }
            }
            if(category.Message !=String.Empty) { return category; }

            var data = _mapper.Map<Category>(dto);

            data.Logo = category.Logo;
            var result = await UnitOfWork.Categories.CreateAsync(data);

            category = _mapper.Map<CategoryDisplayDto>(result);
            return category;


        }

        public async Task<IEnumerable<CategoryDisplayDto>> GetCategoriesData(Expression<Func<Category, bool>> match = null)
        {
            
            if (match == null)
            {
                var categories = await UnitOfWork.Categories.GetAllAsync();
                var data = _mapper.Map<IEnumerable<CategoryDisplayDto>>(categories);
                return data;
            }
            else
            {
                var categories = await UnitOfWork.Categories.GetAllAsync(match);
                if(categories.FirstOrDefault() == null)
                {
                    return null;
                }
                var data = _mapper.Map<IEnumerable<CategoryDisplayDto>>(categories);
                return data;
            }

        }

        public async Task<DeleteDto> DeleteCategory(Guid id)
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(c=>c.Id ==id);
            var types = await UnitOfWork.Types.GetAllAsync(t => t.CategoryId == id);
            var DeleteResult = new DeleteDto() { Message = string.Empty };
            if (categories.FirstOrDefault() == null)
            {
                DeleteResult.Message = $"No Category With ID {id}";
                return DeleteResult;
            }
            if (types.Any())
            {
                DeleteResult.Message = $"The Category Has Types You Shoud Delete It First";
                return DeleteResult;
            }

            await UnitOfWork.Categories.DeleteAsync(categories.First());
            DeleteResult.Message = null;
            return DeleteResult;
        }

        public async Task<CategoryDisplayDto> UpdateCategory(CategoryFormDto dto , Guid id)
        {

            var category = new CategoryDisplayDto() { Message = String.Empty };
            var categories = await UnitOfWork.Categories.GetAsync(c=> c.Id == id);
            if (categories == null)
            {

                category.Message = $"no category with id {id}";
                return category;
            }
            var categorie = await UnitOfWork.Categories.GetAsync(c => c.Name == dto.Name && c.Id != id);
            if (categorie != null)
            {
                category.Message += $"{dto.Name} is already exist";

            }
            
            if (dto.Logo != null)
            {
                var images = new List<IFormFile>();
                images.Add(dto.Logo);
                if (Images.CheckImageValid(images) != null)
                {
                    category.Message += Images.CheckImageValid(images);
                }
                else
                {
                    categories.Logo = Images.ImageToByteArray(images).First();
                }
            }
           
            if (category.Message != String.Empty) { return category; }
            categories.Name = dto.Name;
            _context.SaveChanges();
            category = _mapper.Map<CategoryDisplayDto>(categories);
            category.Message = null;
            
            return category;

        }

        public async Task<DeleteDto> DeleteCategoryAndTypes(Guid id)
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(c => c.Id == id);
            var types = await UnitOfWork.Types.GetAllAsync(t => t.CategoryId == id);
            var DeleteResult = new DeleteDto() { Message = string.Empty };
            if (categories.FirstOrDefault() == null)
            {
                DeleteResult.Message = $"No Category With ID {id}";
                return DeleteResult;
            }
            if (types.Any())
            {
                await UnitOfWork.Types.DeleteRange(types);
              
                
            }

            await UnitOfWork.Categories.DeleteAsync(categories.First());
            DeleteResult.Id = id;
            DeleteResult.Message = null;
            return DeleteResult;
        }
    }
}
