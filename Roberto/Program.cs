using LinqToTwitter;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Roberto
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            IAuthorizer auth = Secrets.DoSingleUserAuth();

            await auth.AuthorizeAsync();

            var twitterCtx = new TwitterContext(auth);

            await ReadTimeLine(twitterCtx);
        }

        static async Task ReadTimeLine(TwitterContext twitterCtx)
        {
            var tweets =
                await
                (from tweet in twitterCtx.Status
                 where tweet.Type == StatusType.Home
                       && tweet.Count == 200
                       && tweet.TweetMode == TweetMode.Extended
                 select tweet)
                .ToListAsync();

            var tweetContents = tweets.Select(tweet => new TweetModel {
                StatusId = tweet.StatusID,
                Text = tweet.FullText,
                UserName = tweet.User.Name,
                CreatedAt = tweet.CreatedAt
            });

            return;
        }

        static async Task TweetAsync(TwitterContext twitterCtx)
        {
            Console.Write("Enter your status update: ");
            string status = Console.ReadLine();

            Console.WriteLine("\nStatus being sent: \n\n\"{0}\"", status);
            Console.Write("\nDo you want to update your status? (y or n): ");
            string confirm = Console.ReadLine();

            if (confirm.ToUpper() == "N")
            {
                Console.WriteLine("\nThis status is *not* being sent.");
            }
            else if (confirm.ToUpper() == "Y")
            {
                Console.WriteLine("\nPress any key to post tweet...\n");
                Console.ReadKey(true);

                Status tweet = await twitterCtx.TweetAsync(status);

                if (tweet != null)
                    Console.WriteLine(
                        "Status returned: " +
                        "(" + tweet.StatusID + ")" +
                        tweet.User.Name + ", " +
                        tweet.Text + "\n");
            }
            else
            {
                Console.WriteLine("Not a valid entry.");
            }
        }

    }
}
