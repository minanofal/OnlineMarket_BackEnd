using AutoMapper;
using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces;
using Market.Core.Models.MarketModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers.MarketControler
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
    

        public CategoriesController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
           
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var data = await _unitOfWork.Categories.GetCategoriesData();
            if (data == null)
            {
                List<CategoryDisplayDto> data2 = new List<CategoryDisplayDto>();
                return Ok(data2);
            }
            return Ok(data.OrderBy(c=>c.Name));
          
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
           if(id == null)
            {
                return BadRequest();
            }
           var category =await _unitOfWork.Categories.GetCategoriesData(c=>c.Id == id);
            if (category == null)
                return NotFound($"No Category with id {id}");
            return Ok(category.FirstOrDefault());
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = await _unitOfWork.Categories.DeleteCategory(id);
            if (result.Message != null)
                return BadRequest(result.Message);
            return Ok(id);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCategoryWithTypes/{id}")]
        public async Task<IActionResult> DeleteCategoryWithTypes(Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var result = await _unitOfWork.Categories.DeleteCategoryAndTypes(id);
            if (result.Message != null)
                return BadRequest(result.Message);
            result.Message = " The Category Deleted With Types";
            return Ok(result);
           
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> DeleteCategory([FromForm]CategoryFormDto dto,Guid id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var data = await _unitOfWork.Categories.UpdateCategory(dto,id);
            if (data.Message != null)
                return BadRequest(data.Message);
            return Ok(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromForm]CategoryFormDto dto)
        {
           if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = await _unitOfWork.Categories.CreateCategory(dto);
            if(data.Message != null)
            {
                return BadRequest(data.Message);
            }
            return Ok(data);
        }
       

    }
}
