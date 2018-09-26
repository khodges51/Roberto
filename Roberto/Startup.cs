using LinqToTwitter;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Roberto.DataStore;
using Roberto.Datastore.Models;
using Datastore.Commands;
using Roberto.Datastore.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Roberto.Services;
using Microsoft.Extensions.Hosting;
using Roberto.HostedServices;

namespace Roberto
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDatabase, Database>(d => new Database(Secrets.ConnectionString));
            services.AddScoped<ITwitterAuthService, TwitterAuthService>();
            services.AddScoped<ITweetService, TweetService>();
            services.AddSingleton<IHostedService, TweetTask>();
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime)
        {
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                appLifetime.StopApplication();
                // Don't terminate the process immediately, wait for the Main thread to exit gracefully.
                eventArgs.Cancel = true;
            };
        }

        private void OnStarted()
        {
            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            // Perform post-stopped activities here
        }
    }
}
