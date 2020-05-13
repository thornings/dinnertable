using ClientMadbordet.Models;
using ClientMadbordet.ViewModels;
using System;
using System.Linq;

namespace ClientMadbordet.Intefaces
{
    public interface ICalendarRepository
    {
        IQueryable<CalendarFoodItem> GetCalendarFoodItemsByDate(DateTime date);
        IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> GetFoodItemInMeal(IQueryable<CalendarFoodItem> foodItems);

        void Save();

    }
}
