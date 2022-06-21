using Market.Core.AuthenticationsInterfaces;
using Market.Core.DTOS.AuthenticationsDTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _auth;

        public AuthenticationController(IAuthenticationRepository auth)
        {
            _auth = auth;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterFormDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _auth.Register(dto);
            if (!result.IsAuthenticated) 
                return BadRequest(result);
            return Ok(result);

        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginFormDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _auth.Login(dto);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);

        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdatePasswordFormDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _auth.UpdatePassword(dto);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);

        }


        [Authorize(Roles ="Admin")]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleFormDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _auth.AddRole(dto);
            if (result.Message != String.Empty)
                return BadRequest(result.Message);
            return Ok(result);

        }

    }
}
