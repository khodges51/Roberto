using Dapper.Contrib.Extensions;
using Roberto.Datastore.Models;
using Roberto.DataStore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Datastore.Commands
{
    public class UpsertTweets : ICommand<bool>
    {

        private IEnumerable<Tweet> _tweets;

        public UpsertTweets(IEnumerable<Tweet> tweets)
        {
            _tweets = tweets;
        }

        public async Task<bool> ExecuteAsync(IDbConnection db)
        {
            using (var transaction = db.BeginTransaction())
            {
                foreach (var tweet in _tweets)
                {
                    try
                    {
                        await db.InsertAsync(tweet, transaction);
                    }
                    catch(SqlException e)
                    {
                        if (!e.Message.Contains("Violation of PRIMARY KEY constraint")) throw e;
                    }
                }

                transaction.Commit();
            }

            return true;
        }
    }
}
