using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Order
    {
        public Order()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        [Key]
        [Column("order_id")] // Specify the column name explicitly
        public int OrderId { get; set; }

        [Column("client_id")]
        public int? ClientId { get; set; }

        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [Column("state")]
        public bool? State { get; set; }

        [ForeignKey("ClientId")] // Specify the foreign key explicitly
        public virtual User Client { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
