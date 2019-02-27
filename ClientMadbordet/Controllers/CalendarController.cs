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

        public ActionResult Index(int year = 0, int month = 0, int day = 0)
        {
            DateTime myDate = GetMyDate(year, month, day);
            ViewBag.myDate = myDate;
            IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> calendarFoodItems = GetFoodItemsInMeals(myDate);
            return View(calendarFoodItems.ToList());
        }

        private IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> GetFoodItems(DateTime myDate)
        {
            var calendarFoodItems =
                from calendarItem in calendarDatabase.FoodItems.Include(fi => fi.Food).Include(fi => fi.Meal)
                join meal in calendarDatabase.Meals on calendarItem.MealID_fk equals meal.MealID
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
            //var calendarFoodItems =
            //                            from calendarItem in calendarDatabase.FoodItems.Include(fi => fi.Food).Include(fi => fi.Meal)
            //                            join meal in calendarDatabase.Meals on calendarItem.MealID_fk equals meal.MealID into me
            //                            from meal in me.DefaultIfEmpty()
            //                            select new
            //                            {
            //                                calenderItems = calendarItem,
            //                                meals = me
            //                            };

            //var calendarFoodItems =

            //    from meal in calendarDatabase.Meals
            //    join calendarItem in calendarDatabase.FoodItems on meal.MealID equals calendarItem.MealID_fk into ci
            //    from calendarItem in ci.DefaultIfEmpty()
            //    select new MealCalendarFoodItemGroup<CalendarFoodItem, string>
            //    {
            //        Key = meal.Name,
            //        Values = ci
            //    };

            var cfi = calendarDatabase.FoodItems.Where(c => c.CalendarDate.Date == myDate.Date).Include(f=>f.Food).Include(m=>m.Meal);
            var calendarFoodItems =
                from meal in calendarDatabase.Meals
                join calendarItem in cfi on meal.MealID equals calendarItem.MealID_fk               
                into ci
                select new MealWithFoodItemsViewModel<CalendarFoodItem, string>
                {
                    Key = meal.Name,
                    Values = ci
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


        //public IActionResult Index()
        //{
        //    var allFoods = fooddb.Foods;
        //    return View(allFoods);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
