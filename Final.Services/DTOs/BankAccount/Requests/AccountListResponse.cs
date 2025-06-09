using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Services.DTOs.BankAccount.Requests
{
    public class AccountListResponse : ResponseDTO
    {
        public List<AccDTO> Accounts { get; set; } = new List<AccDTO>();
    }
}
