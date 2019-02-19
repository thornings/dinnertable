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
        MealContext Mealdb;

        public MealController()
        {
            DbContextOptions options = new DbContextOptions<MealContext>();
            Mealdb = new MealContext(options);
        }

        public IActionResult Index()
        {
            return View(Mealdb.Meals);
        }




    }
}