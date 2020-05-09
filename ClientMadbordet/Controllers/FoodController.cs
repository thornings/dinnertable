using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClientMadbordet.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;

namespace ClientMadbordet.Controllers
{
    public class FoodController : Controller
    {
        private readonly CalendarContext CalendarDb;

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
            ViewBag.WeightTypes = this.CalendarDb.WeightTypes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Food newFood, [FromForm] string[] FoodWeightIds, string[] FoodWeightValues)
        {
 
            if(ModelState.IsValid)
            {
                CalendarDb.Foods.Add(newFood);
                CalendarDb.SaveChanges();

                // add foodweighttypes
                // always gram
                var gramWeighttype = this.CalendarDb.WeightTypes.Where(wt => wt.UnitName == "gram").First();
                var fwt = new FoodWeightType()
                {
                    Food = newFood,
                    FoodId = newFood.FoodID,
                    WeightType = gramWeighttype,
                    WeightTypeId = gramWeighttype.WTID,
                    Weight = Decimal.ToInt32(newFood.Weight)
                };

                for (int i = 0; i < FoodWeightIds.Length; i++)
                {
                    var weight = FoodWeightValues[i];
                    var id = FoodWeightIds[i];
                    if (! string.IsNullOrEmpty(weight))
                    {
                        var weighttype = this.CalendarDb.WeightTypes.Where(wt => wt.WTID == int.Parse(id)).First();
                        fwt = new FoodWeightType()
                        {
                            Food = newFood,
                            FoodId = newFood.FoodID,
                            WeightType = weighttype,
                            WeightTypeId = weighttype.WTID,
                            Weight = int.Parse(weight)
                        };
                        CalendarDb.FoodWeightTypes.Add(fwt);
                    }
                }

                CalendarDb.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WeightTypes = this.CalendarDb.WeightTypes;
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
