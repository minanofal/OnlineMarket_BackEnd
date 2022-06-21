using AutoMapper;
using Market.Core.AuthenticationsInterfaces;
using Market.Core.AuthenticationsModels;
using Market.Core.DTOS.AuthenticationsDTOS;
using Market.Core.ServicesClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.EF.Repostories.AuthRepository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly TokenServices _tokenServices;

       

        public AuthenticationRepository(UserManager<ApplicationUser> userManger , IMapper mapper ,IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager)
        {
            _userManger = userManger;
            _mapper = mapper;
            _tokenServices = new TokenServices(_userManger, jwt);
            _roleManager = roleManager;
        }

        public async Task<AddRoleDto> AddRole(AddRoleFormDto Dto)
        {

            var user = await _userManger.FindByEmailAsync(Dto.Email);
            var role = await _roleManager.FindByNameAsync(Dto.RoleName);
            var roles = new AddRoleDto() { Message = String.Empty};
            if(user == null )
            {
                roles.Message += "Email is Inccorect";
            }
            if(role == null)
            {
                roles.Message += $"The No Role Such {Dto.RoleName}";
            }
            if(roles.Message != String.Empty)
            {
                return roles;
            }

            var userRoles = await _userManger.GetRolesAsync(user);
            var userRolesList = userRoles.ToList();

            if (userRolesList.Contains(Dto.RoleName))
            {
                roles.Message += $"user {user.FirstName} {user.LastName} olready in this Role";
                return roles;
            }

            var result = await _userManger.AddToRoleAsync(user, Dto.RoleName);

            if (! result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += " " + error.Description;
                roles.Message = errors;
                return roles;
            }

            var userRolesresult = await _userManger.GetRolesAsync(user);
            var userRolesListresult = userRolesresult.ToList();

            roles.Email = user.Email;
            roles.RoleNames = userRolesListresult;
            roles.Message = String.Empty;

            return roles;
        }

        public async Task<AuthModelDto> Login(LoginFormDto Dto)
        {
           

            var auth = new AuthModelDto() { Message = string.Empty };
            var user = await _userManger.FindByEmailAsync(Dto.Email);

            ///check The Email And Password

            if (user == null || ! await _userManger.CheckPasswordAsync(user, Dto.Password))
            {
                auth.Message = "Email Or Password Invalid ";
                return auth;
            }
            ///Get The Roles
            var roles = await _userManger.GetRolesAsync(user);

            ///Generate The Token

            var token = await _tokenServices.CreateJwtToken(user);

            ///Set  The values Form Auth Model
            auth.Id = user.Id;
            auth.Email = user.Email;
            auth.FirstName = user.FirstName;
            auth.Roles = roles;
            auth.LastName = user.LastName;
            auth.UserName = user.UserName;
            auth.Expire = token.ValidTo;
            auth.Token = auth.Token = new JwtSecurityTokenHandler().WriteToken(token);
            auth.Message = "Login Successfully !";
            auth.IsAuthenticated = true;
            ////return the Auth Model
            return auth;
        }

        public async Task<AuthModelDto> Register(RegisterFormDto Dto)
        {
            var auth = new AuthModelDto() { Message = String.Empty };
            ///validations
            if(await _userManger.FindByEmailAsync(Dto.Email) != null) {
                auth.Email = "The Email is Aready Exist";
                auth.Message += "The Email aready Exist "; }


            if(await _userManger.FindByNameAsync(Dto.UserName) != null) {
                auth.UserName = "The User Name is Aready Exist";
                auth.Message += "The User Name aready Exist ";}

            if (auth.Message != String.Empty) { return auth; }

            ////Create User

            var user = _mapper.Map<ApplicationUser>(Dto);
            var result = await _userManger.CreateAsync(user,Dto.Password);

            ////check if created successfully 
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += " " + error.Description;
                auth.Message = errors;
                return auth;
            }

            //// insert User To User Role By Default
            await _userManger.AddToRoleAsync(user, "User");

            //// Create Token From Token Services Class in Services in Core 
            var token = await _tokenServices.CreateJwtToken(user);

            ////intial Auth model to send with Token 
            auth.Id = user.Id;
            auth.Email = user.Email;
            auth.Expire = token.ValidTo;
            auth.UserName = user.UserName;
            auth.Roles = new List<string> { "User" };
            auth.FirstName = user.FirstName;
            auth.LastName = user.LastName;
            auth.Message = "Register Successfully !";
            auth.IsAuthenticated = true;
            auth.Token = new JwtSecurityTokenHandler().WriteToken(token);

            ////Send The Model

            return auth;


        }

        public async Task<AuthModelDto> UpdatePassword(UpdatePasswordFormDto Dto)
        {
            var auth = new AuthModelDto() { Message = string.Empty };
            var user = await _userManger.FindByEmailAsync(Dto.Email);

            ///check The Email And Password

            if (user == null || !await _userManger.CheckPasswordAsync(user, Dto.OldPassword))
            {
                auth.Message = "Email Or Password Invalid ";
                return auth;
            }
            ////change password
            var result = await _userManger.ChangePasswordAsync(user,Dto.OldPassword,Dto.NewPassword);
            ///check if success
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += " " + error.Description;
                auth.Message = errors;
                return auth;
            }
            var roles = await _userManger.GetRolesAsync(user);

            ///Generate The Token

            var token = await _tokenServices.CreateJwtToken(user);

            ///Set  The values Form Auth Model

            auth.Email = user.Email;
            auth.FirstName = user.FirstName;
            auth.Roles = roles;
            auth.LastName = user.LastName;
            auth.UserName = user.UserName;
            auth.Expire = token.ValidTo;
            auth.Token = auth.Token = new JwtSecurityTokenHandler().WriteToken(token);
            auth.Message = "Login Successfully !";
            auth.IsAuthenticated = true;
            ////return the Auth Model
            return auth;
        }
    }
}
