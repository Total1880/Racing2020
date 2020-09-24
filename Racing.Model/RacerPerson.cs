using System;

namespace Racing.Model
{
    public class RacerPerson
    {
        public Guid RacerPersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nation Nation { get; set; }
        public int FlatAbility { get; set; }
        public int FlatPotentialAbility { get; set; }
        public int ClimbingAbility { get; set; }
        public int ClimbingPotentialAbility { get; set; }
        public int DownhillAbility { get; set; }
        public int DownhillPotentialAbility { get; set; }
        public int AverageAbility
        {
            get => (FlatAbility + ClimbingAbility + DownhillAbility) / 3;
        }
        public int Age { get; set; }
        public Team Team { get; set; }
        public string Jersey { get; set; }
    }
}
