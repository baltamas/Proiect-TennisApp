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
        public int PlayerRating { get; set; }
        public int PlayerScore { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

            
    }
}
