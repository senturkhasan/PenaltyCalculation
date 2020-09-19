using System;
using System.Collections.Generic;

namespace EntityFrameworkCore2.Models
{
    public class InputData
    {
        public string Name { get; set; }
        public DateTime CheckedDate { get; set; }
        public DateTime ReturnedDate { get; set; }
        public int ResultDay { get; set; }
        public List<KeyValuePair<string, string>> list { get; set; }
    }
}
