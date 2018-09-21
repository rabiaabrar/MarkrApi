using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkrApi.Models
{
    public class Aggregates
    {
        public int TestId { get; set; }
        public decimal Mean { get; set; }
        public int Count { get; set; }
        public decimal P25 { get; set; }
        public decimal P50 { get; set; }
        public decimal P75 { get; set; }
    }
}