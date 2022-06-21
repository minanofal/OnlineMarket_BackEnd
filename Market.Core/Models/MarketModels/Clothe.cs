using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.Models.MarketModels
{
    public class Clothe : Product
    {
        public string Genre { get; set; }
        public IEnumerable<Clothe_Color_Size_count>  clothe_Color_Size_Counts { get; set; }
    }
}
