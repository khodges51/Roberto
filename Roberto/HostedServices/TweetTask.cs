using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roberto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Roberto.HostedServices
{
    public class TweetTask : ScheduledProcessor
    {
        public TweetTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "* */4 * * *"; //Runs every X minutes

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var tweetService =
                    scope.ServiceProvider
                        .GetRequiredService<ITweetService>();

                tweetService.TweetAsync();
            }

            return Task.CompletedTask;
        }
    }
}
