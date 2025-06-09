using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Final.Models;
using Final.Repositories.BaseClasses;
using Final.Repositories.Helpers;
using Final.Repositories.Interfaces.User;

namespace Final.Repositories.Implementations.User
{
    public class UserRepository : BaseRepository<Models.User>, IUserRepository
    {
        private readonly string idFieldName= "UserId";
        public Task<int> CreateAsync(Models.User entity)
        {
            return base.CreateAsync(entity, idFieldName);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(idFieldName, objectId);
        }

        public Task<Models.User> RetrieveAsync(int objectId)
        {
            return RetrieveAsync(idFieldName,objectId);
        }

        public IAsyncEnumerable<Models.User> RetrieveCollectionAsync(UserFilter filter)
        {
            Filter commandFilter = new Filter();
            if (filter.Username is not null) {
            commandFilter.AddCondition("Username", filter.Username);
            }
            if (filter.Fullname is not null) {
            commandFilter.AddCondition("FullName", filter.Fullname);
            }
            if (filter.UserId is not null) {
            commandFilter.AddCondition("UserId", filter.UserId.Value);
            } 

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, UserUpdate update)
        {
            SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            UpdateCommand command = new UpdateCommand(connection, GetTableName(), idFieldName, objectId);
            command.AddSetClause("FullName", update.FullName);
            command.AddSetClause("Password", update.Password);
            return await command.ExecuteNonQueryAsync() > 0;
        }

        protected override string[] GetColumns()
        {
            return new string[]
            {
                "UserId",
                "FullName",
                "Username",
                "Password"
            };
        }

        protected override string GetTableName()
        {
            return "Users";
        }

        protected override Models.User MapEntity(SqlDataReader reader)
        {
            return new Models.User
            {
                UserId = Convert.ToInt32(reader["Userid"]),
                Fullname = Convert.ToString(reader["FullName"]) ?? string.Empty,
                Username = Convert.ToString(reader["Username"]) ?? string.Empty,
                Password = Convert.ToString(reader["Password"]) ?? string.Empty
            };
        }
    }
}
