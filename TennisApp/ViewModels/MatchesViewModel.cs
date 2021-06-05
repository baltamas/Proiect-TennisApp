using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisApp.Models;

namespace TennisApp.ViewModel
{
    public class MatchesViewModel
    {

        public int MatchId { get; set; }

        public string Stage { get; set; }

        public DateTime Date { get; set; }

        public Boolean? Winner { get; set; }

        public int? Player1Id { get; set; }

        public int? Player2Id { get; set; }

    }
}
