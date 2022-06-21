using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.MarketDTOS
{
    public class DeleteDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
