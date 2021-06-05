using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TennisApp.Models
{
    public class Player
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
       // [Range(1.0, 7.0)]
        public double PlayerRating { get; set; }
        public double PlayerScore { get; set; }

            
    }
}
