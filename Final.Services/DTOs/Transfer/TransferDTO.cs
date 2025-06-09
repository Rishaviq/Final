using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Services.DTOs.Transfer
{
    public class TransferDTO
    {
        public int TransferId { get; set; }
      
        public int GoingToId { get; set; }
        
        public int SenderId { get; set; }
      
        public decimal TransferAmount { get; set; }
       
        public required string TransferReason { get; set; }
        public string? TransferStatus { get; set; }
       
        public int UserId { get; set; }
    }
}
