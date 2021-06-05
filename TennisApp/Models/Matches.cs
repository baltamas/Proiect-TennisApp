using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        public int? Player1Id { get; set; }
        public Player Player1 { get; set; }

        public int? Player2Id { get; set; }

        public Player Player2 { get; set; }
        public Boolean? Winner { get; set; }
       
        public int? Dep1Id { get; set; }
        public int? Dep2Id { get; set; }

        [ForeignKey("Dep1Id")]
        public Matches Dep1 { get; set; }

        [ForeignKey("Dep2Id")]
        public Matches Dep2 { get; set; }
        public List<Reviews> Reviews { get; set; }
    }
}
