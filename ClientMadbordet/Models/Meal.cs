using System.ComponentModel.DataAnnotations;

namespace ClientMadbordet.Models
{
    public class Meal
    {

        public Meal()
        {}

        [Key]
        public int MealID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }

}

