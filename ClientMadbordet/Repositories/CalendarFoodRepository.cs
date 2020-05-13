using ClientMadbordet.Interfaces;
using ClientMadbordet.Models;
using ClientMadbordet.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ClientMadbordet.Controllers
{
    public class CalendarFoodRepository : ICalendarFoodRepository
    {
        private readonly CalendarContext _calendarDatabase;

        public CalendarFoodRepository(CalendarContext calendarContext)
        {
            this._calendarDatabase = calendarContext;
        }
     

        public void Save()
        {
            _calendarDatabase.SaveChanges();
        }

        public Meal FindMealById(int mealId)
        {
            return _calendarDatabase.Meals.Find(mealId);
        }

        public IQueryable<CalendarFoodItem> GetFoodItemsByDateAndMeal(DateTime date, int mealId)
        {
            var res = _calendarDatabase.FoodItems
                   .Where(fi => fi.CalendarDate.Date == date.Date && fi.Meal.MealID == mealId)
                   .Include(f => f.SelectedFoodWeightType)
                   .Include(f => f.Food)
                   .ThenInclude(fwt => fwt.FoodWeightTypes)
                   .ThenInclude(wt => wt.WeightType);
            return res;
        }
    }
}
