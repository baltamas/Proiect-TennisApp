using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisApp.ViewModel
{
    public class MatchesViewModel
    {

        public int MatchId { get; set; }

        public string Stage { get; set; }

        public DateTime Date { get; set; }

        public int Player1 { get; set; }
        public int Player2 { get; set; }
        public Boolean Winner { get; set; }

        public string Review { get; set; }
    }
}
