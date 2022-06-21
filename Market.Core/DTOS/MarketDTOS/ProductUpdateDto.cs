using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.MarketDTOS
{
    public class ProductUpdateDto
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }

        public int TotlaCount { get; set; }

        public Guid TypeId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid CompanyId { get; set; }
        public List<IFormFile>? NewproductImages { get; set; }
        public List<Guid>? DeletedImages { get; set; }
    }
}
