using Market.Core.DTOS.AuthenticationsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.AuthenticationsInterfaces
{
    public interface IAuthenticationRepository
    {
        Task<AuthModelDto> Register(RegisterFormDto Dto);
        Task<AuthModelDto> Login(LoginFormDto Dto);
        Task<AuthModelDto> UpdatePassword(UpdatePasswordFormDto Dto);
        Task<AddRoleDto> AddRole(AddRoleFormDto Dto);


    }
}
