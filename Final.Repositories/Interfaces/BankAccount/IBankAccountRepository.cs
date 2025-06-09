using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Repositories.BaseClasses;

namespace Final.Repositories.Interfaces.BankAccount
{
    public interface IBankAccountRepository : IBaseRepository<Models.BankAccount, BankAccountFilter, BankAccountUpdate>
    {
    }
}
