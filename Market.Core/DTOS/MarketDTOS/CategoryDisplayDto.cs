using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.MarketDTOS
{
    public class CategoryDisplayDto
    {
        public Guid Id { get; set; }

        public string Message { get; set; }
        public string Name { get; set; }

        public byte[]? Logo { get; set; }
    }
}
