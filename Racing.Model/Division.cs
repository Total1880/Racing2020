using System.Collections.Generic;

namespace Racing.Model
{
    public class Division
    {
        public int DivisionId { get; set; }
        public int Tier { get; set; }
        public string Name { get; set; }
        public IList<Team> TeamList { get; set; }
    }
}
