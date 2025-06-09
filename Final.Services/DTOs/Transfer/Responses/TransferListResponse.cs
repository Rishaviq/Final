using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Services.DTOs.Transfer.Responses
{
    public class TransferListResponse : ResponseDTO
    {
        public List<TransferDTO> Transfers { get; set; }
        public TransferListResponse()
        {
            Transfers = new List<TransferDTO>();
        }
        
    }
}
