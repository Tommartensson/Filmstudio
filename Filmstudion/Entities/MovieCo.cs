using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Entities
{
    public class MovieCo
    {
        public int id { get; set; }
       
        [Required]
        public string name { get; set; }
      
        [Required]
        public string Password { get; set; }
       
        [Required]
        public string place
        {
            get; set;
        }
        
        public int Number { get; set; }
        
        
        public string nameOfCo { get; set; }
    }
}
