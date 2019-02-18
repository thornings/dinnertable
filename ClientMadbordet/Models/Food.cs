using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMadbordet.Models
{
    public class Food
    {
        public Food()
        {
            //Ingredient = new HashSet<Ingredient>();
            //CalendarItems = new HashSet<CalendarItem>();
        }

        [Required]
        [Key]
        public int FoodID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Protein { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Carb { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fat { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Energy { get; set; }

        //  public virtual ICollection<Ingredients> Ingredients { get; set; }
        // public virtual ICollection<CalendarItem> CalendarItems { get; set; }
    }

}

