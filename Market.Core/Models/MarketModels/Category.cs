using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.Models.MarketModels
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public byte[]? Logo { get; set; }
        public IEnumerable< Type> Types { get; set; }
        public IEnumerable< Company> Companies { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
