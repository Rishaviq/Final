using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Repositories.BaseClasses;

namespace Final.Repositories.Interfaces.UsersPerAcc
{
    public interface IUsersPerAccRepository : IBaseRepository<Models.UsersPerAcc, UsersPerAccFilter, UsersPerAccUpdate>
    {
    }
}
