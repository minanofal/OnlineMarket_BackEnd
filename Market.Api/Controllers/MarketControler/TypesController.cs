using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers.MarketControler
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllTypes()
        {

            var data = await _unitOfWork.Types.GetTypes();

            if (data == null)
            {
                List<TypeDisplayDto> data1 = new List<TypeDisplayDto>();
                return Ok(data1);
            }
            return Ok(data);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Gettype(Guid id)
        {
            var data = await _unitOfWork.Types.GetTypes(t => t.Id == id);
            if (data == null)
                return NotFound($"no Type With Id {id}");
            return Ok(data);

        }
        [HttpGet("TypesOfCategory/{id}")]
        public async Task<IActionResult> GettypesOfCategory(Guid id)
        {
            var category = await _unitOfWork.Categories.GetCategoriesData(c => c.Id == id);
            if (category == null)
            {
                return NotFound($"there is no Category with id {id}");
            }
            var data = await _unitOfWork.Types.GetTypes(t => t.CategoryId == id);

            if (data == null)
            {

                return Ok(new List<TypeDisplayDto>());
            }
            return Ok(data.OrderBy(c => c.Name));

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateType")]
        public async Task<IActionResult> CreateType([FromBody] TypeFormeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _unitOfWork.Types.CreateType(dto);
            if (result.Message != null)
                return BadRequest(result.Message);
            return Ok(result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateType/{id}")]
        public async Task<IActionResult> UpdateType([FromBody] TypeFormeDto dto, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _unitOfWork.Types.UpdateType(id, dto);
            if (result.Message != null)
                return BadRequest(result.Message);
            return Ok(result);

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteType/{id}")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            var result = await _unitOfWork.Types.DeleteType(id);
            if (result == Guid.Empty)
                return NotFound($"No Type With ID {id}");
            return Ok(result);

        }

    }
}
