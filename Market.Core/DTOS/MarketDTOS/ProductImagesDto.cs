using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.MarketDTOS
{
    public class ProductImagesDto
    {
        public Guid Id { get; set; }

        public byte[] Image { get; set; }
}
}
