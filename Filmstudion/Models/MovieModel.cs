using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Filmstudion.Models
{
    public class MovieModel
    {
        
        [Key]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int releaseYear { get; set; }
        [Required]
        public string director { get; set; }
       public int Loanable { get; set; }
        
    }
}
