using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Repositories.BaseClasses;
using Final.Repositories.Helpers;
using Final.Repositories.Interfaces.Transfer;
using Microsoft.Data.SqlClient;

namespace Final.Repositories.Implementations.Transfer
{
    public class TransferRepository : BaseRepository<Models.Transfer>, ITransferRepository
    {
        private readonly string idFieldName = "TransferId";
        public Task<int> CreateAsync(Models.Transfer entity)
        {
            return base.CreateAsync(entity, idFieldName);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Transfer> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(idFieldName, objectId);
        }

        public IAsyncEnumerable<Models.Transfer> RetrieveCollectionAsync(TransferFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Userid is not null)
            {
                commandFilter.AddCondition("UserId", filter.Userid.Value);
            }
            if (filter.GoingToId is not null)
            {
                commandFilter.AddCondition("GoingToId", filter.GoingToId.Value);
            }
            if (filter.SenderId is not null)
            {
                commandFilter.AddCondition("SenderId", filter.SenderId.Value);
            }
            if (filter.TransferStatus is not null)
            {
                commandFilter.AddCondition("TransferStatus", filter.TransferStatus.Value);
            }
            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, TransferUpdate update)
        {
            SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            UpdateCommand command = new UpdateCommand(connection, GetTableName(), idFieldName, objectId);
            command.AddSetClause("TransferStatus", update.TransferStatus);
            return await command.ExecuteNonQueryAsync() > 0;

        }

        protected override string[] GetColumns()
        {
            return new string[] {

               "TransferId",
               "GoingToId",
               "SenderId",
               "TransferAmount",
               "TransferReason",
               "TransferStatus",
               "UserId"
           };
        }

        protected override string GetTableName()
        {
            return "Transfers";
        }

        protected override Models.Transfer MapEntity(SqlDataReader reader)
        {
           return new Models.Transfer
           {
               TransferId = Convert.ToInt32(reader["TransferId"]),
               GoingToId = Convert.ToInt32(reader["GoingToId"]),
               SenderId = Convert.ToInt32(reader["SenderId"]),
               TransferAmount = Convert.ToDecimal(reader["TransferAmount"]),
               TransferReason = Convert.ToString(reader["TransferReason"]) ?? string.Empty,
               TransferStatus = Convert.ToString(reader["TransferStatus"]) ?? string.Empty,
               UserId =Convert.ToInt32(reader["UserId"])
           };
        }
    }
}
