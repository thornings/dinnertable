using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMadbordet.Models
{
    public class Meal
    {
        public Meal()
        {}

        [Required]
        [Key]
        public int ID { get; set; }
        
        //[Required]
        //[StringLength(50,MinimumLength=3)]
        //public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }

}

