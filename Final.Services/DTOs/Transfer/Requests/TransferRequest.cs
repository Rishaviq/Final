using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Services.DTOs.Transfer.Requests
{
    public class TransferRequest
    {
        [Required]
        public required string GoingToAccNumber { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public decimal TransferAmount { get; set; }
        [Required]
        public required string TransferReason { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
