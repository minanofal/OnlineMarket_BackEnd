using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.Models.MarketModels
{
    public class Type
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Company> Companies { get; set; }

         public List<Company_Type> company_Types { get; set; }
    }
}
