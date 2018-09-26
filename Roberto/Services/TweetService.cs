using Datastore.Commands;
using LinqToTwitter;
using Roberto.Datastore.Models;
using Roberto.Datastore.Queries;
using Roberto.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roberto.Services
{
    public class TweetService : ITweetService
    {

        private ITwitterAuthService _twitterAuthService;
        private IDatabase _database;

        public TweetService(ITwitterAuthService twitterAuthService, IDatabase database)
        {
            _twitterAuthService = twitterAuthService;
            _database = database;
        }

        public async Task ReadTimeLineAsync()
        {
            var twitterCtx = await _twitterAuthService.GetTwitterContext();

            var tweets =
                await
                (from tweet in twitterCtx.Status
                 where tweet.Type == StatusType.Home
                       && tweet.Count == 200
                       && tweet.TweetMode == TweetMode.Extended
                 select tweet)
                .ToListAsync();

            var tweetContents = tweets.Select(tweet => new Tweet
            {
                StatusId = (long)tweet.StatusID,
                Text = tweet.FullText,
                UserName = tweet.User.Name,
                CreatedAt = tweet.CreatedAt
            });

            var mostRecentStatusIds = await _database.QueryAsync(new GetMostRecentStatusIds());

            var tweetsToUpsert = tweetContents.Where(tweet => !mostRecentStatusIds.Contains(tweet.StatusId));

            await _database.CommandAsync(new UpsertTweets(tweetsToUpsert));
        }

        public async Task TweetAsync()
        {
            var twitterCtx = await _twitterAuthService.GetTwitterContext();

            Status tweet = await twitterCtx.TweetAsync($"Hello World. The date and time is {DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt")}.");

            if (tweet != null)
                Console.WriteLine(
                    "Status returned: " +
                    "(" + tweet.StatusID + ")" +
                    tweet.User.Name + ", " +
                    tweet.Text + "\n");
        }
    }
}
