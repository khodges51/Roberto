using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roberto.Services
{
    public interface ITweetService
    {
        Task ReadTimeLineAsync();

        Task TweetAsync();
    }
}
