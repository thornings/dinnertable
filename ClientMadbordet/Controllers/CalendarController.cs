using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClientMadbordet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ClientMadbordet.ViewModels;

namespace ClientMadbordet.Controllers
{
    public class CalendarController : Controller
    {
        private CalendarContext calendarDatabase;

        public CalendarController()
        {
            var calendaroptions = new DbContextOptionsBuilder<CalendarContext>();
            this.calendarDatabase = new CalendarContext(calendaroptions.Options);
        }

        public ActionResult Index(int year, int month, int day )
        {
            DateTime myDate = GetMyDate(year, month, day);
            IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> calendarFoodItems = GetFoodItemsInMeals(myDate);
            var calendarMeals = this.calendarDatabase.Meals;

            CalendarViewModel cvm = new CalendarViewModel()
            {
                CalendarFoodItems = calendarFoodItems,
                Meals = calendarMeals,
                TheDate = myDate,
                TheDateText = myDate.Year + "/" + myDate.Month + "/" + myDate.Day
            };
            
            return View(cvm);
        }

        private IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> GetFoodItems(DateTime myDate)
        {
            var calendarFoodItems =
                from calendarItem in calendarDatabase.FoodItems.Include(fi => fi.Food).Include(fi => fi.Meal)
                join meal in calendarDatabase.Meals on calendarItem.Meal.MealID equals meal.MealID
                where calendarItem.CalendarDate.Date == myDate.Date
                group calendarItem by calendarItem.Meal.Name
                into calendarGroup
                select new MealWithFoodItemsViewModel<CalendarFoodItem, string>
                {
                    Key = calendarGroup.Key,
                    Values = calendarGroup
                };
            return calendarFoodItems;
        }

        private IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> GetFoodItemsInMeals(DateTime myDate)
        {
            var cfi = calendarDatabase.FoodItems.Where(c => c.CalendarDate.Date == myDate.Date).Include(f=>f.Food).Include(m=>m.Meal);
            var calendarFoodItems =
                from meal in calendarDatabase.Meals
                join calendarItem in cfi on meal.MealID equals calendarItem.Meal.MealID               
                into ci
                select new MealWithFoodItemsViewModel<CalendarFoodItem, string>
                {
                    Key = meal.Name,
                    Values = ci,
                    Id = meal.MealID
                };
            return calendarFoodItems;
        }


        private static DateTime GetMyDate(int year, int month, int day)
        {
            DateTime myDate;

            // test parameters exists
            if (year == 0 || (month) == 0 || day == 0)
            {
                myDate = DateTime.Now;
                myDate = new DateTime(myDate.Year, myDate.Month, myDate.Day, 0, 0, 0);
            }
            else
            {
                myDate = new DateTime(year, month, day, 0, 0, 0);
            }

            return myDate;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
