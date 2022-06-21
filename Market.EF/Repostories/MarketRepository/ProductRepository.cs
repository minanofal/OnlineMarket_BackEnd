using AutoMapper;
using Market.Core.Consts;
using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces.IUnitOfWorkS;
using Market.Core.InterFaces.MarketInterfaces.UnitOfWorkS;
using Market.Core.Models.MarketModels;
using Market.EF.Repostories.MarketRepository.UnitOfWorkS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Market.EF.Repostories.MarketRepository
{
    public class ProductRepository : IProductRepository
    {
        public IUnitOfWorkForEntities UnitOfWork { get; private set; }
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            UnitOfWork = new UnitOfWorkForEntities(_context);
            _mapper = mapper;
        }
        public async Task<ProductDisplayDto> CreateProduct(ProductFormDto dto)
        {
            var product = new ProductDisplayDto() { Message = string.Empty};
            var productCheck = await UnitOfWork.Products.GetAllAsync(p => p.Name == dto.Name);
            if (productCheck.Any())
            {
                product.Message += $"{dto.Name} is already exist";
            }
            var category = await UnitOfWork.Categories.GetAllAsync(c => c.Id == dto.CategoryId);
            if (!category.Any())
            {
                product.Message += $" No Category With Id {dto.CategoryId}";
            }
            var type = await UnitOfWork.Types.GetAllAsync(t => t.Id == dto.TypeId);
            if (!type.Any())
            {
                product.Message += $" No Type With Id {dto.TypeId}";
            }
            var type_category = await UnitOfWork.Types.GetAllAsync(t=>t.Id == dto.TypeId && t.CategoryId == dto.CategoryId);
            if (!type_category.Any())
            {
                product.Message += $" No Type With id {dto.TypeId} in category with id {dto.CategoryId} ";
            }
            var company = await UnitOfWork.Companies.GetAllAsync(c=> c.Id == dto.CompanyId);
            if (!company.Any()) 
            {

                product.Message += $" No Company With Id {dto.CompanyId}";
            }
            var company_category = await UnitOfWork.Companies.GetAllAsync(c => c.Id == dto.CompanyId && c.CategoryId == dto.CategoryId);
            if (!company.Any())
            {

                product.Message += $" No Company With Id {dto.CompanyId} in Category with id {dto.CategoryId}";
            }
            var images = new List<byte[]>();
            if (dto.productImages.Any())
            {
                if (Images.CheckImageValid(dto.productImages) != null)
                {
                    product.Message += Images.CheckImageValid(dto.productImages);
                }
                else
                {
                    images = Images.ImageToByteArray(dto.productImages);
                }
            }
            if(product.Message != String.Empty)
            {
                return product;
            }


            var data = _mapper.Map<Product>(dto);
            
            var result = await UnitOfWork.Products.CreateAsync(data);

            foreach(var img in images)
            {
               await UnitOfWork.ProductImages.CreateAsync(new ProductImage { ProductId = data.Id , Image = img});

            }
            var type_company = await UnitOfWork.Company_type.GetAllAsync(ct=>ct.CompaneyId == dto.CompanyId && ct.TypeId == dto.TypeId);
            if (!type_company.Any())
            {
               await UnitOfWork.Company_type.CreateAsync(new Company_Type { CompaneyId = dto.CompanyId , TypeId = dto.TypeId });
            }
       
            product = _mapper.Map<ProductDisplayDto>(data);
            var imgs = await UnitOfWork.ProductImages.GetAllAsync(i => i.ProductId == data.Id);
            product.productImages = _mapper.Map<IEnumerable<ProductImagesDto>>(imgs);
            product.Company = company.First().Name;
            product.Type = type.First().Name;
            product.Category = category.First().Name;
            product.Message = null;
            return product;
        }

        public async Task<IEnumerable<ProductDisplayDto>> GetProducts(Expression<Func<Product, bool>> match = null)
        {
            if(match == null)
            {
                var data = await UnitOfWork.Products.GetAllAsync(null , new[] { "productImages" , "Category", "Company", "Type" } );
                if(data == null)
                {
                    return null;
                }
                var product =  data.Select(p => new ProductDisplayDto 
                { Id = p.Id ,
                  CategoryId = p.CategoryId ,
                  CompanyId = p.CompanyId ,
                  Name = p.Name,
                  TypeId = p.TypeId ,
                  Description = p.Description ,
                  Created = p.Created ,
                  Updated = p.Updated,
                  Price = p.Price,
                  TotlaCount = p.TotlaCount,
                  productImages = p.productImages.Select(i =>new ProductImagesDto { Id = i.Id , Image = i.Image} ).ToList(),
                  Type = p.Type.Name,
                  Category = p.Category.Name,
                  Company = p.Company.Name              
                });
                return product;               
            }
            else
            {
                var data = await UnitOfWork.Products.GetAllAsync(match, new[] { "productImages", "Category", "Company", "Type" });
                if (data == null)
                {
                    return null;
                }
                var product = data.Select(p => new ProductDisplayDto
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    CompanyId = p.CompanyId,
                    Name = p.Name,
                    TypeId = p.TypeId,
                    Description = p.Description,
                    Created = p.Created,
                    Updated = p.Updated,
                    Price = p.Price,
                    TotlaCount = p.TotlaCount,
                    productImages = p.productImages.Select(i => new ProductImagesDto { Id = i.Id, Image = i.Image }).ToList(),
                    Type = p.Type.Name,
                    Category = p.Category.Name,
                    Company = p.Company.Name
                });
                return product;
            }
          
        }

        public async Task<DeleteDto> DeleteProduct(Guid id)
        {
            var result = new DeleteDto() { Message = String.Empty };
            var product = await UnitOfWork.Products.GetAsync(p => p.Id == id);
            if(product == null)
            {
                result.Message = $"There is no product with id {id}";
                return result;
            }
           await UnitOfWork.ProductImages.DeleteRange(i => i.ProductId == id);
            await UnitOfWork.Carts.DeleteRange(c => c.ProductId == id);
           await UnitOfWork.Products.DeleteAsync(product);
            result.Id = id;
            result.Message = null; 
            return result;
        }

        public async Task<ProductDisplayDto> UpdateProduct(ProductUpdateDto dto, Guid id)
        {
            var product = await UnitOfWork.Products.GetAsync(p => p.Id == id);
            var result = new ProductDisplayDto() { Message = String.Empty };

            if(product == null)
            {
                result.Message += $"There is no Product with id {id} ";
                return result;
            }
            var category = await UnitOfWork.Categories.GetAllAsync(c => c.Id == dto.CategoryId);
            if (!category.Any())
            {
                result.Message += $" No Category With Id {dto.CategoryId}";
            }
            var type = await UnitOfWork.Types.GetAllAsync(t => t.Id == dto.TypeId);
            if (!type.Any())
            {
                result.Message += $" No Type With Id {dto.TypeId}";
            }
            var type_category = await UnitOfWork.Types.GetAllAsync(t => t.Id == dto.TypeId && t.CategoryId == dto.CategoryId);
            if (!type_category.Any())
            {
                result.Message += $" No Type With id {dto.TypeId} in category with id {dto.CategoryId} ";
            }
            var company = await UnitOfWork.Companies.GetAllAsync(c => c.Id == dto.CompanyId);
            if (!company.Any())
            {

                result.Message += $" No Company With Id {dto.CompanyId}";
            }
            var company_category = await UnitOfWork.Companies.GetAllAsync(c => c.Id == dto.CompanyId && c.CategoryId == dto.CategoryId);
            if (!company.Any())
            {

                result.Message += $" No Company With Id {dto.CompanyId} in Category with id {dto.CategoryId}";
            }
            var images = new List<byte[]>();
            if (dto.NewproductImages!=null)
            {
                if (Images.CheckImageValid(dto.NewproductImages) != null)
                {
                    result.Message += Images.CheckImageValid(dto.NewproductImages);
                }
                else
                {
                    images = Images.ImageToByteArray(dto.NewproductImages);
                }
            }
            if (dto.DeletedImages!=null)
            {
                foreach (var image in dto.DeletedImages)
                {
                    var checkimage = await UnitOfWork.ProductImages.GetAllAsync(i => i.Id == image);
                    if (!checkimage.Any())
                    {
                        result.Message += $"  No Image With ID id {image}";
                    }
                }
            }
            if (result.Message != String.Empty)
            {
                return result;
            }

            if (dto.DeletedImages!=null)
            {
                foreach(var image in dto.DeletedImages)
                {
                    await UnitOfWork.ProductImages.DeleteRange(i => i.Id == image);
                }
            }

            foreach (var img in images)
            {
                await UnitOfWork.ProductImages.CreateAsync(new ProductImage { ProductId = id, Image = img });

            }
            var type_company = await UnitOfWork.Company_type.GetAllAsync(ct => ct.CompaneyId == dto.CompanyId && ct.TypeId == dto.TypeId);
            if (!type_company.Any())
            {
                await UnitOfWork.Company_type.CreateAsync(new Company_Type { CompaneyId = dto.CompanyId, TypeId = dto.TypeId });
            }

            product.Updated = DateTime.Now;
            product.CategoryId = dto.CategoryId;
            product.CompanyId = dto.CompanyId;
            product.TypeId = dto.TypeId;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;
            product.TotlaCount = dto.TotlaCount;
            await _context.SaveChangesAsync();

            result = _mapper.Map<ProductDisplayDto>(product);
            var imgs = await UnitOfWork.ProductImages.GetAllAsync(i => i.ProductId == id);
            result.productImages = _mapper.Map<IEnumerable<ProductImagesDto>>(imgs);
            result.Company = company.First().Name;
           result.Type = type.First().Name;
            result.Category = category.First().Name;
            result.Message = null;
            

            return result;

            
        }
    }
}
