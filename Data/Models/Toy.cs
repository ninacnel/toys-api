using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Toy
    {
        public Toy()
        {
            OrderLines = new HashSet<OrderLine>();
            PriceHistory = new HashSet<PriceHistory>();
        }

        [Key]
        [Column("code")] // Specify the column name explicitly
        public int Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("toy_img")]
        public byte[] ToyImg { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("stock_threshold")]
        public int StockThreshold { get; set; }

        [Column("state")]
        public bool? State { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [ForeignKey("CategoryId")] // Specify the foreign key explicitly
        public virtual Category Category { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual ICollection<PriceHistory> PriceHistory { get; set; }
    }
}
