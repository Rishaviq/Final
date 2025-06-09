using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Final.Repositories.Helpers
{
    public class UpdateCommand : IDisposable
    {
        public string idColumnName;
        public int idColumnValue;
        public List<string> setClauses;
        public SqlCommand command;
        public UpdateCommand(SqlConnection connection, string tableName, string _idColumnName, int _idColumnValue)
        {


            idColumnName = _idColumnName;
            idColumnValue = _idColumnValue;
            setClauses = new List<string>();
            command = connection.CreateCommand();
            command.CommandText = $"UPDATE {tableName}";
        }

        public void AddSetClause(string dbFieldName, INullable? dbFieldValue)
        {
            if (dbFieldValue is not null)
            {
                setClauses.Add($"{dbFieldName} = @{dbFieldName}");
                command.Parameters.AddWithValue($"@{dbFieldName}", dbFieldValue);
            }
        }

        public async Task<int> ExecuteNonQueryAsync()
        {
            if (setClauses.Count == 0)
            {
                throw new Exception("No fields to update! You should pass at least one!");
            }

            command.CommandText +=
@$" SET {string.Join(", ", setClauses)}
WHERE {idColumnName} = @{idColumnName}";

            command.Parameters.AddWithValue($"@{idColumnName}", idColumnValue);

            SqlTransaction transaction = command.Connection.BeginTransaction();
            command.Transaction = transaction;

            int rowsAffected = await command.ExecuteNonQueryAsync();
            try
            {
                if (rowsAffected != 1)
                {
                    transaction.Rollback();
                    throw new Exception($"Just one row should be updated! Command aborted, because {rowsAffected} could have been updated!");

                }

                transaction.Commit();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"An error occurred while executing the update command: {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            command.Dispose();
        }

    }
}
