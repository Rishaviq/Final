using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Repositories.Interfaces.UsersPerAcc
{
    public class UsersPerAccFilter
    {
        public SqlInt32? UserId { get; set; }
        public SqlInt32? BankAccountId { get; set; }
    }
}
