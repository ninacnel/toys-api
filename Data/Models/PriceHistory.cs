using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class PriceHistory
    {
        [Key]
        [Column("history_id")] // Specify the column name explicitly
        public int HistoryId { get; set; }

        [Column("toy_code")]
        public int? ToyCode { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("change_date")]
        public DateTime ChangeDate { get; set; }

        [ForeignKey("ToyCode")] // Specify the foreign key explicitly
        public virtual Toy ToyCodeNavigation { get; set; }
    }
}
