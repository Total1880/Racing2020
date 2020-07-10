using System.ComponentModel.DataAnnotations;

namespace Racing.Model
{
    public class LastNames
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public Nation Nation { get; set; }
    }
}
