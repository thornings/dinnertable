using ClientMadbordet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMadbordet.ViewModels
{
    public class CalendarFoodViewModel
    {
        public Meal Meal { get; set; }
        public DateTime Date { get; set; }
        public string DateFormatted { get; set; }
        public List<Food> Foods;
        public IQueryable<CalendarFoodItem> FoodItems;
        public string queryString { get; set; }
        public int TotalWeight { get; set; }
        public decimal TotalEnergy { get; set; }
    }
}