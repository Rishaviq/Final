using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Models
{
    public class UsersPerAcc
    {
        public int RecordId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BankAccountId { get; set; }
    }
}
