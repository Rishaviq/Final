using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Services.DTOs.BankAccount
{
    public class AccDTO
    {
        public int AccId { get; set; }
        [Required]
        [StringLength(22)]
        public required string AccNumber { get; set; }
        [Required]
        public decimal AccBalance { get; set; }
    }
}
