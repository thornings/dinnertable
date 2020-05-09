using ClientMadbordet.Models;
using ClientMadbordet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMadbordet.Controllers
{
    public interface ICalendarRepository
    {
        IQueryable<CalendarFoodItem> GetCalendarFoodItemsByDate(DateTime date);
        IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> GetFoodItemInMeal(IQueryable<CalendarFoodItem> foodItems);

        void Save();

    }
}
