using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50)]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50)]
        public required string Password { get; set; }

        [Required]
        [StringLength(50)]
        public required string Fullname { get; set; }
    }
}
