using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.Models.MarketModels
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public int TotlaCount { get; set; }
        public Type Type { get; set; }
        public Guid TypeId  { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public IEnumerable<ProductImage> productImages { get; set; }
        public IEnumerable<Cart> Carts { get; set; }

    }
}
