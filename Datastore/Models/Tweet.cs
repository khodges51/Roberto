using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Roberto.Datastore.Models
{
    public partial class Tweet
    {
        [ExplicitKey]
        public Int64 StatusId { get; set; }

        public string Text { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
