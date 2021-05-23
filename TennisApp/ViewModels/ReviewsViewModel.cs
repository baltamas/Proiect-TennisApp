using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisApp.Models;

namespace TennisApp.ViewModels
{
    public class ReviewsViewModel
    {
        
        public int ReviewId { get; set; }
        
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

    }
}
