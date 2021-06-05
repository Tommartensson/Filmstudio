﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Models
{
    public class AdminModel
    {
 
        [Required]
        public string Username;
        [Required]
        public string password;
        
    }
}
