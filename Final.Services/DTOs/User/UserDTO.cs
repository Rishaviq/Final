using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Services.DTOs.User
{
    public class UserDTO
    {
        public int UserId { get; set; }     
        public required string Username { get; set; }
        public required string Email { get; set; }
    }
}
