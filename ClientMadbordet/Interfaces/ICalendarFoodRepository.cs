using ClientMadbordet.Models;
using ClientMadbordet.ViewModels;
using System;
using System.Linq;

namespace ClientMadbordet.Interfaces
{
    public interface ICalendarFoodRepository
    {
        Meal FindMealById(int mealId);
        IQueryable<CalendarFoodItem> GetFoodItemsByDateAndMeal(DateTime date, int mealId);
        void Save();

    }
}
