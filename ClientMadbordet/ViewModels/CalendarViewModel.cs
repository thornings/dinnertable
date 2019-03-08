using ClientMadbordet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMadbordet.ViewModels
{
    public class CalendarViewModel
    {
        public IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> CalendarFoodItems { get; set; }
        public IQueryable<Meal> Meals { get; set; }
        public DateTime TheDate { get; set; }
        public string TheDateText { get; set; }
    }
}