using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roberto.Services
{
    public interface ITwitterAuthService
    {
        Task<TwitterContext> GetTwitterContext();
    }
}
