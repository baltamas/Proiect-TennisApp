using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisApp.Models;

namespace TennisApp.ViewModels
{
    public class MatchesWithReviewsViewModel
    {
        public int MatchId { get; set; }

        public string Stage { get; set; }

        public DateTime Date { get; set; }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Boolean Winner { get; set; }

        public List<ReviewsViewModel> Reviews { get; set; }
    }
}
