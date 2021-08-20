using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Entities
{
    public class Movie
    {
       [Key]
        public int MovieId { get; set; }
        public string name { get; set; }
        public int releaseYear { get; set; }
        public string country { get; set; }
        public string director { get; set; }
        public int Loanable { get; set; }
        [PrimaryKey]
        public int LoanId { get; set; }
        
    }
}
