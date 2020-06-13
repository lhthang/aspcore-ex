using cinema_core.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_core.Models
{
    public class User : BaseEntity
    {

        [Required]
        [MaxLength(20, ErrorMessage = "Username can not be more than 20 character")]
        public string Username { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Fullanme can not be more than 200 character")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
