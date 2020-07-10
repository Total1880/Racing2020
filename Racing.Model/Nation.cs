using System.ComponentModel.DataAnnotations;

namespace Racing.Model
{
    public class Nation
    {
        [Key]
        public int NationId { get; set; }
        public string Name { get; set; }
    }
}
