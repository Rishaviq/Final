using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Repositories.BaseClasses;

namespace Final.Repositories.Interfaces.Transfer
{
    public interface ITransferRepository : IBaseRepository<Models.Transfer, TransferFilter, TransferUpdate>
    {
    }
}
