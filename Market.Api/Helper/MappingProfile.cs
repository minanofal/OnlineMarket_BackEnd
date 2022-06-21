using AutoMapper;
using Market.Core.AuthenticationsModels;
using Market.Core.DTOS.AuthenticationsDTOS;
using Market.Core.DTOS.MarketDTOS;
using Market.Core.Models.MarketModels;
using Type = Market.Core.Models.MarketModels.Type;

namespace Market.Api.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterFormDto, ApplicationUser>();


            CreateMap<CategoryFormDto, Category>().ForMember(src => src.Logo, opt => opt.Ignore());
            CreateMap<Category, CategoryDisplayDto>();

            CreateMap<TypeFormeDto, Type>();
            CreateMap<Type, TypeDisplayDto>();
        
            CreateMap<CompanyFormDto,Company>().ForMember(src => src.Image, opt => opt.Ignore()).ForMember(src => src.Types, opt => opt.Ignore());
            CreateMap<Company, CompanyDisplayDto>().ForMember(src => src.Types , opt => opt.Ignore());


            CreateMap<ProductFormDto, Product>().ForMember(src => src.productImages, opt => opt.Ignore());
            CreateMap<Product, ProductDisplayDto>().ForMember(src => src.productImages, opt => opt.Ignore() ).
                ForMember(src => src.Type, opt => opt.Ignore()).
                ForMember(src => src.Category, opt => opt.Ignore()).
                ForMember(src => src.Company, opt => opt.Ignore());

            CreateMap<ProductImage, ProductImagesDto>();

            CreateMap<AddCartDto, Cart>();

            CreateMap<Product, CartProductsDto>()
                .ForMember(s => s.Id, opt => opt.Ignore())
                .ForMember(s => s.Created, opt => opt.Ignore())
                .ForMember(s => s.Count, opt => opt.Ignore())
                .ForMember(s => s.ProductId, opt => opt.MapFrom(s => s.Id));
                
        }
    }
}
