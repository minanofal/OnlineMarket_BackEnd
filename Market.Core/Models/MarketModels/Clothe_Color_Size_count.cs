using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Core.Models.MarketModels
{
    public class Clothe_Color_Size_count
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClotheId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int count { get; set; }
        public Clothe Clothe { get; set; }

    }
}
