using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Repositories.BaseClasses;
using Final.Repositories.Helpers;
using Final.Repositories.Interfaces.BankAccount;
using Microsoft.Data.SqlClient;

namespace Final.Repositories.Implementations.BankAccount
{
    public class BankAccountRepository : BaseRepository<Models.BankAccount>, IBankAccountRepository
    {
        private readonly string idFieldName = "AccId";
        public Task<int> CreateAsync(Models.BankAccount entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.BankAccount> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(idFieldName, objectId);
        }

        public IAsyncEnumerable<Models.BankAccount> RetrieveCollectionAsync(BankAccountFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.AccNumber is not null)
            {
                commandFilter.AddCondition("AccNumber", filter.AccNumber.Value);
            }
            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, BankAccountUpdate update)
        {
            SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            UpdateCommand command = new UpdateCommand(connection, GetTableName(), idFieldName, objectId);
            command.AddSetClause("AccBalance", update.AccBalance);
            return await command.ExecuteNonQueryAsync() > 0;

        }

        protected override string[] GetColumns()
        {
            return new string[] {
            "AccId",
            "AccNumber",
            "AccBalance"
            };
        }

        protected override string GetTableName()
        {
            return "BankAccounts";
        }

        protected override Models.BankAccount MapEntity(SqlDataReader reader)
        {
            return new Models.BankAccount
            {
                AccId = Convert.ToInt32(reader["AccId"]),
                AccNumber = Convert.ToString(reader["AccNumber"]) ?? string.Empty,
                AccBalance = Convert.ToDecimal(reader["AccBalance"])
            };
        }
    }
}
