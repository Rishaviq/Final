using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Repositories.Interfaces.Transfer
{
    public class TransferFilter
    {
        public SqlInt32? Userid { get; set; }
        public SqlInt32? GoingToId { get; set; }
        public SqlInt32? SenderId { get; set; }
        public SqlString? TransferStatus { get; set; }
    }
}
