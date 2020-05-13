using ClientMadbordet.Models;
using ClientMadbordet.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMadbordet.Controllers
{
    internal class CalendarFoodLogic
    {
        public CalendarFoodLogic()
        {
        }

        public int FoodItemsTotalWeight(List<CalendarFoodItem> foodItems)
        {
            return foodItems.Sum(f => f.Weight);
        }

        public decimal FoodItemsTotalEnergy(List<CalendarFoodItem> foodItems)
        {
            var energy = 0;

            energy = foodItems
                .Where(f => f.SelectedFoodWeightType != null)
                .Sum(f => f.SelectedFoodWeightType.Weight * f.Weight);

            //  weight = gram;           
            energy += foodItems
                .Where(f => f.SelectedFoodWeightType == null)
                .Sum(f => f.Weight);

            return energy;
        }

        private SelectListItem GetDefaultGramSelectListItem(bool isGramSelected)
        {
            string defaultName = "Gram";
            string defaultId = "1";
            return new SelectListItem(defaultName, defaultId, isGramSelected);
        }

        public List<SelectListItem> CreateSelectedListItems(CalendarFoodItem fi)
        {
            var foodWeightTypes = new List<SelectListItem>();

            // Gram Weighttype
            bool isGramSelected = fi.SelectedFoodWeightType == null;
            var gram = GetDefaultGramSelectListItem(isGramSelected);
            foodWeightTypes.Add(gram);

            foreach (var fwt in fi.Food.FoodWeightTypes)
            {              
                var item = new SelectListItem { Text = fwt.WeightType.UnitName, Value = fwt.WeightTypeId.ToString() };
                if (!isGramSelected && fi.SelectedFoodWeightType.FoodWeightTypeID == fwt.FoodWeightTypeID)
                {
                    item.Selected = true;
                }

                foodWeightTypes.Add(item);
            }
            return foodWeightTypes;
        }
    }
}