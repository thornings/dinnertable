using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClientMadbordet.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientMadbordet.Controllers
{
    public class HomeController : Controller
    {
        FoodContext madbordetdb;

        public HomeController()
        {
            var options = new DbContextOptionsBuilder<FoodContext>();
            var context = new FoodContext(options.Options);
            this.madbordetdb = context;
        }

        public IActionResult Index()
        {
            var allFoods = madbordetdb.Foods;
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
                madbordetdb.Foods.Add(newFood);
                madbordetdb.SaveChanges();
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
            Food food = madbordetdb.Foods.Find(id);

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
                madbordetdb.Foods.Update(food);
                madbordetdb.SaveChanges();
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

            Food food = madbordetdb.Foods.Find(id);

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

            var food = madbordetdb.Foods.Find(id);

            if (food == null)
            {
                return NotFound();
            }

            madbordetdb.Foods.Remove(food);
            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
