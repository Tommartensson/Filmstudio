using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Entities
{
    public class Admin : IdentityUser
    {
        
        public string Username;
        
        public string password;
        
        public int Number;
    }
}
