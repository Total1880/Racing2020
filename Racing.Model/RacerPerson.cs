namespace Racing.Model
{
    public class RacerPerson
    {
        public int RacerPersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nation Nation { get; set; }
        public int Ability { get; set; }
        public Team Team { get; set; }
    }
}
