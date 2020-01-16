using ClientMadbordet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientMadbordet.Controllers
{
    public class MealController : Controller
    {
        CalendarContext calendarDb;

        public MealController(CalendarContext cDb)
        {
            calendarDb = cDb;
        }

        public IActionResult Index()
        {
            var meals = calendarDb.Meals;
            return View(meals);
        }
    }
}