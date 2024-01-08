using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("roles")] // Specify the table name explicitly
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Column("role_id")] // Specify the column name explicitly
        [Key]
        public int RoleId { get; set; }

        [Column("role_name")]
        public string RoleName { get; set; }

        // Navigation property
        public virtual ICollection<User> Users { get; set; }
    }
}
