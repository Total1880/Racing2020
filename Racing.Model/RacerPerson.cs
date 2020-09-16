using System;

namespace Racing.Model
{
    public class RacerPerson
    {
        public Guid RacerPersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nation Nation { get; set; }
        public int Ability { get; set; }
        public int PotentialAbility { get; set; }
        public int Age { get; set; }
        public Team Team { get; set; }
        public string Jersey { get; set; }
    }
}
