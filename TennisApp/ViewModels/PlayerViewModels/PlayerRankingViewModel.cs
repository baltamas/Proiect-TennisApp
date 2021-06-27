using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisApp.ViewModels.PlayerViewModels
{
    public class PlayerRankingViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public int PlayerRating { get; set; }
        public int PlayerScore { get; set; }

        public int Ranking { get; set; }
    }
}
