using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Services.DTOs.BankAccount;

namespace Final.Services.DTOs.Transfer.Responses
{
    public class GetAccountResponse : ResponseDTO
    {
        public AccDTO? Account { get; set; }
    }
}
