using ClientMadbordet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ClientMadbordet.ViewModels
{
    public class CalendarFoodItemViewModel
    {
        public CalendarFoodItem FoodItem { get; set; }
        public List<SelectListItem> FoodWeightTypes { get; set; }
        public SelectListItem SelectedWeightType { get; set; }
    }
}