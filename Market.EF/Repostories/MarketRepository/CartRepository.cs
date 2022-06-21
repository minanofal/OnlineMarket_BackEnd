using AutoMapper;
using Market.Core.AuthenticationsModels;
using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces;
using Market.Core.InterFaces.MarketInterfaces.UnitOfWorkS;
using Market.Core.Models.MarketModels;
using Market.EF.Repostories.MarketRepository.UnitOfWorkS;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.EF.Repostories.MarketRepository
{
    public class CartRepository : ICartRepository
    {
        public IUnitOfWorkForEntities UnitOfWork { get; private set; }
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManger;

        public CartRepository(ApplicationDbContext context, IMapper mapper , UserManager<ApplicationUser> userManger)
        {
            _context = context;
            _userManger = userManger;
            _mapper = mapper;
            UnitOfWork = new UnitOfWorkForEntities(_context);
        }

        public async Task<bool> AddToCart(AddCartDto dto)
        {
            var product = await UnitOfWork.Products.GetAsync(p => p.Id == dto.ProductId);
            if (product == null) return false;
            if( await _userManger.FindByIdAsync(dto.UserId) == null) return false;
            if(dto.Count == 0 || product.TotlaCount < dto.Count) return false;
            product.TotlaCount = (int)(product.TotlaCount - dto.Count);
            var cart = _mapper.Map<Cart>(dto);
            await UnitOfWork.Carts.CreateAsync(cart);
            await _context.SaveChangesAsync();
            return true;
           
           
        }

        public async Task<IEnumerable<CartProductsDto>> GetCartProducts(string UserId)
        {
           
            if(await _userManger.FindByIdAsync(UserId) == null)
            {
                return null;
            }

            var data = await UnitOfWork.Carts.GetAllAsync(c => c.UserId == UserId, new[] { "Product" });
            var imgs = await UnitOfWork.ProductImages.GetAllAsync();
            var results = data.Select(p => new CartProductsDto
            {
                ProductId = p.ProductId,
                Count = (int)p.Count,
                Created = p.Added,
                Id = p.Id,
                PeacePrice = p.Product.Price,
                TotalPrice = p.Count * p.Product.Price,
                ProductName = p.Product.Name,
                Image = imgs.Where(i=> i.ProductId == p.ProductId).First().Image.ToArray()


            }) ;
            

            return results;
            
        }

        public async Task<bool> RemoveFromCart(Guid id)
        {
            var cart = await UnitOfWork.Carts.GetAsync(c => c.Id == id);
            if (cart == null)
                return false;
            var product = await UnitOfWork.Products.GetAsync(p => p.Id == cart.ProductId);
            product.TotlaCount += (int)cart.Count;
            _context.SaveChanges();
            await UnitOfWork.Carts.DeleteAsync(cart);
            return true;
        }
    }
}
