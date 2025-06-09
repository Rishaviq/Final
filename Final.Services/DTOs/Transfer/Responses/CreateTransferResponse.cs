using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Services.DTOs.Transfer.Responses
{
    public class CreateTransferResponse : ResponseDTO
    {
        public int CreatedTransferId { get; set; }
    }
}
