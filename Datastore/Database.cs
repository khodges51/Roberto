using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Roberto.DataStore
{
    public class Database : IDatabase
    {
        readonly string _connectionString;

        public Database(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public static void SetupDefaults()
        {
            SqlMapperExtensions.TableNameMapper = (type) =>
            {
                //Dapper uses plural table names by default. Use singluar.
                return type.Name;
            };

            Dapper.SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);
        }

        public async Task<T> QueryAsync<T>(IQuery<T> query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await query.ExecuteAsync(connection);
            }
        }

        public async Task<T> CommandAsync<T>(ICommand<T> command)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await command.ExecuteAsync(connection);
            }
        }
    }
}
