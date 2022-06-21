using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.AuthenticationsDTOS
{
    public class AddRoleDto
    {
        public string Email { get; set; }

        public List<string> RoleNames { get; set; }

        public string Message { get; set; }
    }
}
