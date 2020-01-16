using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClientMadbordet.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientMadbordet.Controllers
{
    public class FoodController : Controller
    {
        CalendarContext CalendarDb;

        public FoodController(CalendarContext db)
        {
            CalendarDb = db;
        }

        public IActionResult Index()
        {
            var allFoods = CalendarDb.Foods;
            return View(allFoods);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Food newFood)
        {
            if(ModelState.IsValid)
            {
                CalendarDb.Foods.Add(newFood);
                CalendarDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newFood);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Food food = CalendarDb.Foods.Find(id);

            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind]Food food)
        {
            if (ModelState.IsValid)
            {
                CalendarDb.Foods.Update(food);
                CalendarDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            Food food = CalendarDb.Foods.Find(id);

            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = CalendarDb.Foods.Find(id);

            if (food == null)
            {
                return NotFound();
            }

            CalendarDb.Foods.Remove(food);
            CalendarDb.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
