using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iMed.Models
{
    public class Announcement
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Hour { get; set; }
        public string Date { get; set; }
    }
}
