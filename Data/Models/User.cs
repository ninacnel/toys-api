using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("user_id")] // Specify the column name explicitly
        public int UserId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role_id")]
        public int? RoleId { get; set; }

        [Column("state")]
        public bool? State { get; set; }

        [ForeignKey("RoleId")] // Specify the foreign key explicitly
        public virtual Role Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
