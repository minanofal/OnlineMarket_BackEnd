using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.MarketDTOS
{
    public class ProductDisplayDto
    {
        public string Message { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Description { get; set; }

        public int TotlaCount { get; set; }

        public Guid TypeId { get; set; }
        public string Type { get; set; }

        public Guid CategoryId { get; set; }
        public string Category { get; set; }
        public Guid CompanyId { get; set; }
        public string Company { get; set; }
        public IEnumerable<ProductImagesDto> productImages { get; set; }
    }
}
