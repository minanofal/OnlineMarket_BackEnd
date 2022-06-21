using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.DTOS.MarketDTOS
{
    public class CategoryFormDto
    {   [MaxLength(50)]
        public string Name { get; set; }

        public IFormFile? Logo { get; set; }
    }
}
