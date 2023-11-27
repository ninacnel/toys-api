using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class CategoryViewModel
    {
        public int category_code { get; set; }
        public string category_name { get; set; }
        public bool? state { get; set; }
    }
}
