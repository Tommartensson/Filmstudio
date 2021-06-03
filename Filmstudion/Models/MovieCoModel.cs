using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Models
{
    public class MovieCoModel : IdentityUser
    {
        
        [Required]
        public string name { get; set; }
        
        public string place
        {
            get; set;
        }
        [Required]
        public string Password { get; set; }
        public int Number { get; set; }
        //public string email { get; set; }
        public string nameOfCo { get; set; }
    }
}
