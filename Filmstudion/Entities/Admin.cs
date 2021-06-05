using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Entities
{
    public class Admin
    {
        
        public string Username { get; set; }
        
        public string password { get; set; }

        public int Number { get; set; }
    }
}
