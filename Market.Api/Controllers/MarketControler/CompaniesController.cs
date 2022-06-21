using AutoMapper;
using Market.Core.DTOS.MarketDTOS;
using Market.Core.InterFaces.MarketInterfaces;
using Market.EF.Repostories.MarketRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers.MarketControler
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompaniesController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
     
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var data = await _unitOfWork.Companies.GetAllCompanyDisplays();
            if (data == null)
            {
                List<CompanyDisplayDto> data2 = new List<CompanyDisplayDto>();
                return Ok(data2);
            }
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanies(Guid id)
        {
            var data = await _unitOfWork.Companies.GetAllCompanyDisplays(c => c.Id == id);
            if (data == null)
            {

                return NotFound($"No Comapany with id {id}");
            }
            return Ok(data);

        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetCategoryCompanies(Guid id)
        {
            var category = await _unitOfWork.Categories.GetCategoriesData(c => c.Id == id);
            if (category == null)
            {
                return NotFound($"there is no Category with id {id}");
            }
            var data = await _unitOfWork.Companies.GetAllCompanyDisplays(c => c.CategoryId == id);
            if (data == null)
            {

                
                return Ok(new List<CompanyDisplayDto>());
            }
            return Ok(data.OrderBy(c=>c.Name));

        }
        [HttpGet("type/{id}")]
        public async Task<IActionResult> GettypeCompanies(Guid id)
        {
            var category = await _unitOfWork.Types.GetTypes(c => c.Id == id);
            if (category == null)
            {
                return NotFound($"there is no type with id {id}");
            }
            var data = await _unitOfWork.Companies.GetAllCompanyDisplays(c => c.Types.Select(c => c.Id).Contains(id));
            if (data == null)
            {

            
                return Ok(new List<CompanyDisplayDto>());
            }
            return Ok(data.OrderBy(c => c.Name));

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCompan/{id}")]
        public async Task<IActionResult> deleteCompanies(Guid id)
        {
            var data = await _unitOfWork.Companies.DeleteCompanyAndTypes(id);
            if (data == null)
            {
                return NotFound($"There is No Company with id {id}");
            }
            return Ok(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateComapn/{id}")]
        public async Task<IActionResult> UpdateCompany([FromForm] CompanyFormDto dto,Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var data = await _unitOfWork.Companies.UpdateCompany(dto,id);
            if (data.Message != null)
            {
                return BadRequest(data.Message);
            }
            return Ok(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateComapny")]
        public async Task<IActionResult> CreateCompany([FromForm] CompanyFormDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var data = await _unitOfWork.Companies.CreateCompany(dto);
            if (data.Message != null)
            {
                return BadRequest(data.Message);
            }
            return Ok(data);
        }

    }
}
