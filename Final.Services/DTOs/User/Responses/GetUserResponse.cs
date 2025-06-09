using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office.Services.DTOs.User
{
    public class GetUserResponse : ResponseDTO
    {
        public UserDTO? User { get; set; }
    }
}
