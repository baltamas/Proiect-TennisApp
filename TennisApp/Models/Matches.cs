using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TennisApp.Models
{
    public class Matches
    {
        [Key]
        public int MatchId { get; set; }
        [Required]
        public string Stage { get; set; }
        [Required]
        public DateTime Date { get; set; }
        
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Boolean Winner { get; set; }
       
        public List<Reviews> Reviews { get; set; }
    }
}
