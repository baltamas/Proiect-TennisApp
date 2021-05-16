using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisApp.ViewModels
{
    public class PlayerViewModel
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public double PlayerRating { get; set; }
        public double PlayerScore { get; set; }

    }
}
