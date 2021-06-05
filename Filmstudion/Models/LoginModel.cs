using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Models
{
    public class AdminModel : IdentityUser
    {
 
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
        
    }
}
