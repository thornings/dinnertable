using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClientMadbordet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ClientMadbordet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace ClientMadbordet.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private CalendarContext _calendarDatabase;

        public CalendarController(CalendarContext dbc)
        {
            this._calendarDatabase = dbc;
        }

        public ActionResult Index(int year, int month, int day )
        {
            DateTime myDate = DateTime.Now;
            try
            {
                myDate = GetMyDate(year, month, day);
            }
            catch (Exception)
            {
                myDate = DateTime.Now;
            }

            IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> calendarFoodItems = GetFoodItemsInMeals(myDate);
            var meals = this._calendarDatabase.Meals;

            List<CalendarMealViewModel> calendarMealsViewModel = new List<CalendarMealViewModel>();
            foreach (var cm in meals)
            {
                calendarMealsViewModel.Add(
                    new CalendarMealViewModel()
                    {
                        Meal = cm,
                        TotalCarbs = 12,
                        TotalProteins = 14,
                        TotalFats = 15
                    }
                );
            }

            CalendarViewModel cvm = new CalendarViewModel()
            {
                CalendarFoodItems = calendarFoodItems,
                Meals = calendarMealsViewModel,
                TheDate = myDate,
                TheDateText = myDate.Year + "/" + myDate.Month + "/" + myDate.Day,             
            };
            
            return View(cvm);
        }

        private IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> GetFoodItems(DateTime myDate)
        {
            var calendarFoodItems =
                from calendarItem in _calendarDatabase.FoodItems.Include(fi => fi.Food).Include(fi => fi.Meal)
                join meal in _calendarDatabase.Meals on calendarItem.Meal.MealID equals meal.MealID
                where calendarItem.CalendarDate.Date == myDate.Date
                group calendarItem by calendarItem.Meal.Name
                into calendarGroup
                select new MealWithFoodItemsViewModel<CalendarFoodItem, string>
                {
                    Key = calendarGroup.Key,
                    Values = calendarGroup,

                };
            return calendarFoodItems;
        }

        private IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> GetFoodItemsInMeals(DateTime myDate)
        {
            var calendarFoodItem = _calendarDatabase.FoodItems.Where(c => c.CalendarDate.Date == myDate.Date).Include(f=>f.Food).Include(m=>m.Meal);
            var calendarFoodItems =
                from meal in _calendarDatabase.Meals
                join calendarItem in calendarFoodItem on meal.MealID equals calendarItem.Meal.MealID
                into ci
                select new MealWithFoodItemsViewModel<CalendarFoodItem, string>
                {
                    Key = meal.Name,
                    Values = ci,
                    Id = meal.MealID,
                    TotalCarbs= (int)ci.Sum(x=>x.Food.Carb*(((decimal)x.Weight)/100)),
                    TotalProteins = (int)ci.Sum(x=>x.Food.Protein*((decimal)x.Weight/100)),
                    TotalFats = (int)ci.Sum(x=>x.Food.Fat*((decimal)x.Weight/100))
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
