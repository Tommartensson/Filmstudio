using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Models
{
    public class MovieModel
    {
        [Required]
        public string name { get; set; }
        [Required]
        public int releaseYear { get; set; }
        [Required]
        public string country { get; set; }
        [Required]
        public string director { get; set; }
        
    }
}
