using System;
using System.Linq;
using System.Threading.Tasks;
using ClientMadbordet.Models;
using ClientMadbordet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientMadbordet.Controllers
{
    public class CalendarFoodController : Controller
    {
        private CalendarContext calendarDatabase;

        public CalendarFoodController()
        {
            var calendarOptions = new DbContextOptionsBuilder<CalendarContext>();
            this.calendarDatabase = new CalendarContext(calendarOptions.Options);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add(int year, int month, int day, int mealId, string searchString = "")
        {
            CalendarFoodViewModel viewModel = new CalendarFoodViewModel();

            viewModel.queryString = searchString;

            viewModel.Meal = calendarDatabase.Meals.Find(mealId);
            if (viewModel.Meal == null)
            {
                return RedirectToAction("/");
            }

            var date = new DateTime(year, month, day);
            if (date == null)
            {
                return RedirectToAction("/calendar");
            }
            viewModel.DateFormatted = date.Year + "/" + date.Month + "/" + date.Day;
            viewModel.Date = date;

            var foods =
                from m in calendarDatabase.Foods
                select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                foods = foods.Where(s => s.Name.Contains(searchString));
            }
            else
            {
                foods = calendarDatabase.Foods;
            }

            viewModel.Foods = await foods.ToListAsync();
            viewModel.FoodItems = calendarDatabase.FoodItems.Where(fi => fi.CalendarDate.Date == date.Date && fi.Meal.MealID == mealId );

            return View(viewModel);
        }


        //[Bind]
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        [HttpGet]
        public IActionResult Create(int year, int month, int day, int foodId, int mealId)
        {
            var date = new DateTime(year, month, day);
            ViewData["Date"] = date;

            Food food = calendarDatabase.Foods.Find(foodId);
            Meal meal = calendarDatabase.Meals.Find(mealId);

            if (food == null || meal == null)
            {
                TempData["Error"] = "Wrong input";
                return Redirect(
                    string.Format("/calendarFood/add/{0}/{1}/{2}/{3}",
                    year, month, day, mealId));
            }
            CalendarFoodItem foodItem = new CalendarFoodItem()
            {
                Food = food,
                Meal = meal,
                Weight = decimal.ToInt32(food.Weight),
                CalendarDate = date
            };

            calendarDatabase.FoodItems.Add(foodItem);
            calendarDatabase.SaveChanges();

            TempData["Success"] = "Created.";

            return Redirect(
                string.Format("/calendarFood/add/{0}/{1}/{2}/{3}",
                year, month, day, mealId));
        }

        [HttpDelete]        
        public IActionResult Delete(string back, int id)
        { 
            CalendarFoodItem foodItem = calendarDatabase.FoodItems.Find(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            TempData["back"] = back;
            return View(foodItem);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string back, int id)
        {
            back = Request.Form["back"];
            CalendarFoodItem foodItem = calendarDatabase.FoodItems.Find(id);
            calendarDatabase.FoodItems.Remove(foodItem);
            calendarDatabase.SaveChanges();
            return Redirect("/CalendarFood/Add/" + back);
        }
    }
}