using System.ComponentModel.DataAnnotations;

namespace Racing.Model
{
    public class FirstNames
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public Nation Nation { get; set; }
    }
}
