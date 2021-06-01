﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Entities
{
    public class Movie
    {
        public int id { get; set; }
        public string name { get; set; }
        public int releaseYear { get; set; }
        public string country { get; set; }
        public string director { get; set; }

        public bool Borrowed { get; set; }
    }
}