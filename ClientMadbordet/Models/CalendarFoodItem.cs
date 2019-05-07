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
        public int Weight { get; set; }

        //[Required]
        //public int Pieces { get; set; }

        public virtual Food Food { get; set; }
        public virtual Meal Meal { get; set; }
    }
}

