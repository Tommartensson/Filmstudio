using Filmstudion.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Entities
{
    public class MovieCo
    {
        [Key]
        public int MovieCoid { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string place
        {
            get; set;
        }
        public string mail { get; set; }
        public int Number { get; set; }

        public string nameOfCo { get; set; }


        public List<Movie> MyMovies { get; set; }

        public MovieCo()
            {
            MyMovies = new List<Movie>();
            }
    }
}
