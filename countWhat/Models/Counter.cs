using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace countWhat.Models
{
    public class Counter
    {
        public string Id { get; set; }
        public string What { get; set; }
        public long Value { get; set; }
        public string OwnerId { get; set; }
    }
}