using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientMadbordet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientMadbordet.Controllers
{
    public class MealController : Controller
    {
        CalendarContext CalendarDb;

        public MealController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CalendarContext>();
            CalendarDb = new CalendarContext(optionsBuilder.Options);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}