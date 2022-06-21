using Market.Core.InterFaces.MarketInterfaces;
using Market.Core.Models.MarketModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers.MarketControler
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;


        public CartsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        [HttpPost("AddCart")]
        public async Task<IActionResult> adddCart([FromBody] AddCartDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
           var result = await _unitOfWork.Carts.AddToCart(dto);
            if(!result)
                return BadRequest(result);
            return Ok(result);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getUserCart(string id)
        {
            var result = await _unitOfWork.Carts.GetCartProducts(id);
            if (result == null)
                return NotFound("There is no user with this id");
            return Ok(result);
        }

        [HttpGet("count/{id}")]
        public async Task<IActionResult> getUserCartcount(string id)
        {
            var result = await _unitOfWork.Carts.GetCartProducts(id);
            if (result == null)
                return NotFound("There is no user with this id");
            return Ok(result.Count());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteCart(Guid id)
        {
            var result = await _unitOfWork.Carts.RemoveFromCart(id);
            if (!result)
                return NotFound("There is no user with this id");
            return Ok(result);
        }

    }
}
