using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ToyDTO
    {
        public int code { get; set; }
        public string name { get; set; }
        public int? category_id { get; set; }
        public string description { get; set; }
        //public byte[] toy_img { get; set; }
        public int stock { get; set; }
        public int stock_threshold { get; set; }
        public bool? state { get; set; }
        public decimal price { get; set; }
        //public byte[] qr_code { get; set; }
        public List<PriceDTO> PriceHistory { get; set; }
    }
}