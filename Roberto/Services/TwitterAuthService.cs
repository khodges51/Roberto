using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace Roberto.Services
{
    public class TwitterAuthService : ITwitterAuthService
    {
        public async Task<TwitterContext> GetTwitterContext()
        {
            IAuthorizer auth = Secrets.DoSingleUserAuth();
            await auth.AuthorizeAsync();
            return new TwitterContext(auth);
        }
    }
}
