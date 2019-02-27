using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMadbordet.Models
{
    public class CalendarFoodItem
    {
        public CalendarFoodItem()
        {}

        [Key]
        public int CalendarFoodItemID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CalendarDate { get; set; }

        [Required]
        public int FoodID_fk { get; set; }

        [Required]
        public int MealID_fk { get; set; }

        [Required]
        public int Weight { get; set; }

        [ForeignKey("FoodID_fk")]
        public virtual Food Food { get; set; }

        [ForeignKey("MealID_fk")]
        public virtual Meal Meal { get; set; }

    }

}

