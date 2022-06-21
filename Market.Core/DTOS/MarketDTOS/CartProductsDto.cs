using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.MarketDTOS
{
    public class CartProductsDto
    {
        public Guid Id { get; set; }
        public  Guid ProductId { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public DateTime Created { get; set; }
        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal PeacePrice { get; set; }
        public byte[]?  Image { get; set; }

    }
}
