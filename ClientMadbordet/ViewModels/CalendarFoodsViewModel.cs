using ClientMadbordet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMadbordet.ViewModels
{
    public class CalendarFoodsViewModel
    {
        
        public int MealId { get; set; }        
        public List<Food> Foods { get; set; }
        public string DateFormatted { get; set; }

        //public CalendarFoodsViewModel(int mealId, List<Food> foods)
        //{
        //    MealId = mealId;
        //    Foods = foods;
        //}
    }
}