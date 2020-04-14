﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMadbordet.Models
{
    public class CalendarFoodItem
    {
        public CalendarFoodItem()
        { }

        [Key]
        public int CalendarFoodItemID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CalendarDate { get; set; }

        [Required]
        public int Weight { get; set; }

        public virtual Food Food { get; set; }
        public virtual Meal Meal { get; set; }

        [NotMapped]
        public decimal TotalEnergy
        {
            get
            {
                return Food.Energy * WeightPercentage();
            }
        }
        
        [NotMapped]
        public int Carbs
        {
            get
            {
                return (int)(Food.Carb * WeightPercentage());
            }
        }

        [NotMapped]
        public int Proteins
        {
            get
            {
                return (int)(Food.Protein * WeightPercentage());
            }
        }
        
        [NotMapped]
        public int Fats
        {
            get
            {
                return (int)(Food.Fat * WeightPercentage());
            }
        }

        private decimal WeightPercentage()
        {
            return ((decimal)Weight) / 100;
        }
    }
}

