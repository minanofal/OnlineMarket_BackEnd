using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.Models.MarketModels
{
    public class Company_Type
    {
        public Guid CompaneyId { get; set; }
        public Company Companey { get; set; }
        public Guid TypeId { get; set; }
        public Type Type { get; set; }
    }
}
