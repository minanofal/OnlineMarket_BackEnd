using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.MarketDTOS
{
    public class TypeFormeDto
    {
        public Guid CategoryId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
