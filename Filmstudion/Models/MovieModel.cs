using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Models
{
    public class MovieModel
    {
        public string name { get; set; }
        public int releaseYear { get; set; }
        public string country { get; set; }
        public string director { get; set; }
        public bool Borrowed { get; set; }
    }
}
