using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TennisApp.Models
{
    public class Reviews
    {[Key]
        public int ReviewId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public Matches Matches { get; set; }

    }
}
