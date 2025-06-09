using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Repositories.Interfaces.BankAccount
{
    public class BankAccountFilter
    {
        public SqlString? AccNumber { get; set; }
        
    }
}
