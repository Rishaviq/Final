using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Repositories.BaseClasses;
using Final.Repositories.Helpers;
using Final.Repositories.Interfaces.UsersPerAcc;
using Microsoft.Data.SqlClient;

namespace Final.Repositories.Implementations.UsersPerAcc
{
    public class UsersPerAccRepository : BaseRepository<Models.UsersPerAcc>, IUsersPerAccRepository
    {
        private readonly string idFieldName = "RecordId";
        public Task<int> CreateAsync(Models.UsersPerAcc entity)
        {
            return base.CreateAsync(entity, idFieldName);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(idFieldName, objectId);
        }

        public Task<Models.UsersPerAcc> RetrieveAsync(int objectId)
        {
           return base.RetrieveAsync(idFieldName, objectId);
        }

        public IAsyncEnumerable<Models.UsersPerAcc> RetrieveCollectionAsync(UsersPerAccFilter filter)
        {
            Filter commandFilter = new Filter();
            if (filter.UserId is not null)
            {
                commandFilter.AddCondition("UserId", filter.UserId.Value);
            }
            if (filter.BankAccountId is not null)
            {
                commandFilter.AddCondition("BankAccountId", filter.BankAccountId.Value);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public Task<bool> UpdateAsync(int objectId, UsersPerAccUpdate update)
        {
            throw new NotImplementedException();
        }

        protected override string[] GetColumns()
        {
            return new string[] {
                "RecordId",
                "UserId",
                "BankAccountId"
            
            };
        }

        protected override string GetTableName()
        {
            return "UsersPerAccs";
        }

        protected override Models.UsersPerAcc MapEntity(SqlDataReader reader)
        {
            return new Models.UsersPerAcc
            {
                RecordId = Convert.ToInt32(reader["RecordId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                BankAccountId = Convert.ToInt32(reader["BankAccountId"])
            };
        }
    }
}
