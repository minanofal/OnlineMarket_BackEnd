using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.AuthenticationsDTOS
{
    public class AuthModelDto
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Token { get; set; }

        public DateTime Expire { get; set; }


    }
}
