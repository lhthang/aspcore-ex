using cinema_core.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Models
{
    public class Role : BaseEntity
    {

        [Required]
        [MaxLength(20, ErrorMessage = "Name cannot be more than 20 characters")]
        public string Name { get; set; }

        public ICollection<UserRole> UsersRole { get; set; }
    }
}
