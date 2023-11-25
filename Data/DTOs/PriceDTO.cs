using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class PriceDTO
    {
        public int? toy_code { get; set; }
        public decimal price { get; set; }
        public DateTime change_date { get; set; }

    }
}
