using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Category
    {
        [Key]
        [Column("category_code")]
        public int CategoryCode { get; set; }

        [Column("category_name")]
        [Required]
        public string CategoryName { get; set; }

        [Column("state")]
        public bool? State { get; set; }

        // Navigation property
        public virtual ICollection<Toy> Toys { get; set; }
    }
}
