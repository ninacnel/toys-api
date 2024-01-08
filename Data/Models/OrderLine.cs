using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OrderLine
    {
        [Key, Column("order_id", Order = 0)] // Specify the primary key and column names explicitly
        public int OrderId { get; set; }

        [Key, Column("order_line_id", Order = 1)] // Specify the primary key and column names explicitly
        public int OrderLineId { get; set; }

        [Column("toy_code")]
        public int? ToyCode { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("sub_total")]
        public decimal SubTotal { get; set; }

        [Column("wrapped")]
        public bool? Wrapped { get; set; }

        [ForeignKey("OrderId")] // Specify the foreign key explicitly
        public virtual Order Order { get; set; }

        [ForeignKey("ToyCode")] // Specify the foreign key explicitly
        public virtual Toy ToyCodeNavigation { get; set; }
    }
}
