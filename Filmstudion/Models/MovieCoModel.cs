using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Models
{
    public class MovieCoModel
    {
        
        [Required]
        public string name { get; set; }
        
        public string place
        {
            get; set;
        }
        
        public int Number { get; set; }
        public string Email { get; set; }
        public string nameOfCo { get; set; }
    }
}
