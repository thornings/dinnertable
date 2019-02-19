using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClientMadbordet.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientMadbordet.Controllers
{
    public class FoodController : Controller
    {
        FoodContext fooddb;

        public FoodController()
        {
            var options = new DbContextOptionsBuilder<FoodContext>();
            var context = new FoodContext(options.Options);
            this.fooddb = context;
        }

        public IActionResult Index()
        {
            var allFoods = fooddb.Foods;
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
                fooddb.Foods.Add(newFood);
                fooddb.SaveChanges();
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
            Food food = fooddb.Foods.Find(id);

            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Food food)
        {
            if (id != food.FoodID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                fooddb.Foods.Update(food);
                fooddb.SaveChanges();
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

            Food food = fooddb.Foods.Find(id);

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

            var food = fooddb.Foods.Find(id);

            if (food == null)
            {
                return NotFound();
            }

            fooddb.Foods.Remove(food);
            fooddb.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
