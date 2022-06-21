using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.AuthenticationsDTOS
{
    public class AddRoleFormDto
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
