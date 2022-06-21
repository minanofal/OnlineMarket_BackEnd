using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.Models.MarketModels
{
    public class AddCartDto
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Count { get; set; }
    }
}
