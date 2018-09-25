using Dapper;
using Roberto.DataStore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Roberto.Datastore.Queries
{
    public class GetMostRecentStatusIds : IQuery<IEnumerable<Int64>>
    {
        public async Task<IEnumerable<Int64>> ExecuteAsync(IDbConnection db)
        {
            var sql = @"SELECT TOP (1000) [StatusId]
                          FROM [Tweet]
                          ORDER BY [DateTimeRead] DESC";

            var result = await db.QueryAsync<long>(sql);

            return result;
        }
    }
}
