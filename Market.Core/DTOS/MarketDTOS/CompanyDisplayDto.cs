using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.MarketDTOS
{
    public class CompanyDisplayDto
    {
        public string Message { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[]? Image { get; set; }
        public Guid CategoryId { get; set; }
        public List<Guid>  Types { get; set; }
    }
}
