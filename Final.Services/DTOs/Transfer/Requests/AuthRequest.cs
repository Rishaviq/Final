using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Services.DTOs.Transfer.Requests
{
    public class AuthRequest
    {
        public int UserId { get; set; }
        public int TransferId { get; set; }
    }
}
