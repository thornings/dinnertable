using ClientMadbordet.Models;
using ClientMadbordet.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ClientMadbordet.Controllers
{
    public class CalendarRepository : ICalendarRepository
    {
        private CalendarContext _calendarDatabase;


        public CalendarRepository(CalendarContext calendarContext)
        {
            _calendarDatabase = calendarContext;
        }

        public IQueryable<CalendarFoodItem> GetCalendarFoodItemsByDate(DateTime date)
        {
            return _calendarDatabase.FoodItems.Where(c => c.CalendarDate.Date == date.Date).Include(f => f.Food).Include(m => m.Meal);
        }

        public IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> GetFoodItemInMeal(IQueryable<CalendarFoodItem> foodItems)
        {
            var calendarFoodItems =
                from meal in _calendarDatabase.Meals
                join calendarItem in foodItems on meal.MealID equals calendarItem.Meal.MealID
                into ci
                select new MealWithFoodItemsViewModel<CalendarFoodItem, string>
                {
                    Key = meal.Name,
                    Values = ci,
                    Id = meal.MealID,
                    TotalCarbs = (int)ci.Sum(x => x.Food.Carb * (((decimal)x.Weight) / 100)),
                    TotalProteins = (int)ci.Sum(x => x.Food.Protein * ((decimal)x.Weight / 100)),
                    TotalFats = (int)ci.Sum(x => x.Food.Fat * ((decimal)x.Weight / 100))
                };
            return calendarFoodItems;
        }

        public void Save()
        {
            _calendarDatabase.SaveChanges();
        }
    }
}
