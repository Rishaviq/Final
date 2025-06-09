using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Services.DTOs;

namespace Final.Services.DTOs.User
{
    public class LoginResponse : ResponseDTO
    {
        public int UserId { get; set; }
    }
}
