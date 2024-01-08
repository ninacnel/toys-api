using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public bool? State { get; set; }
    }
}
