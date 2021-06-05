using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisApp.Models;

namespace TennisApp.ViewModels
{
    public class MatchViewModel
    {
        public int Id { get; set; }
        public string Stage { get; set; }
        public DateTime Date { get; set; }
        public int? Dep1Id { get; set; }
        public int? Dep2Id { get; set; }
        public Matches Dep1 { get; set; }
        public Matches Dep2 { get; set; }
    }
}
