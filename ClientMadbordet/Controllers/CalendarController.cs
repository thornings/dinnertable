using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClientMadbordet.Models;
using System;
using System.Linq;
using ClientMadbordet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using ClientMadbordet.Utils;

namespace ClientMadbordet.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly CalendarContext _calendarDatabase;
        private readonly CalendarRepository _calendarRepository;

        public CalendarController(CalendarContext dbc)
        {
            this._calendarDatabase = dbc;
            _calendarRepository = new CalendarRepository(this._calendarDatabase);
        }

        public IActionResult Index(int year, int month, int day )
        {
            DateTime calendarDate = DateTimeHelper.SetDateTimeOrDefault(year, month, day);
          
            IQueryable<CalendarFoodItem> foodItemsByDateQuery = _calendarRepository.GetCalendarFoodItemsByDate(calendarDate);
            IQueryable<MealWithFoodItemsViewModel<CalendarFoodItem, string>> calendarFoodItemViewModels = _calendarRepository.GetFoodItemInMeal(foodItemsByDateQuery);

            CalendarViewModel cvm = new CalendarViewModel()
            {
                CalendarFoodItems = calendarFoodItemViewModels,

                TheDate = calendarDate,
                TheDateText = calendarDate.Year + "/" + calendarDate.Month.ToString("d2") + "/" + calendarDate.Day,
            };

            return View(cvm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
