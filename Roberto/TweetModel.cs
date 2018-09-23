﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roberto
{
    public class TweetModel
    {
        public ulong StatusId { get; set; }

        public string Text { get; set; }
        
        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
