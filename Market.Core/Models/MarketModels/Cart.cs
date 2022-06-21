using Market.Core.AuthenticationsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.Models.MarketModels
{
    public class Cart
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Count { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;
    }
}
