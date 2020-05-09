using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientMadbordet.Models;
using ClientMadbordet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClientMadbordet.Controllers
{
    public class CalendarFoodController : Controller
    {
        private readonly CalendarContext CalendarDatabase;

        public CalendarFoodController(CalendarContext db)
        {
            this.CalendarDatabase = db;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add(int year, int month, int day, int mealId)
        {
            CalendarFoodViewModel viewModel = new CalendarFoodViewModel
            {
                Meal = CalendarDatabase.Meals.Find(mealId)
            };
            if (viewModel.Meal == null)
            {
                return RedirectToAction("/");
            }

            DateTime date = DateTime.Now; // default

            try
            {
                date = new DateTime(year, month, day);
                if (date == null)
                {
                    return RedirectToAction("/calendar");
                }

            }
            catch (Exception)
            {
                return Redirect("/calendar");
            }


            viewModel.DateFormatted = date.Year + "/" + date.Month + "/" + date.Day;
            viewModel.Date = date;

            var foods =
                from m in this.CalendarDatabase.Foods
                select m;

            viewModel.Foods = await foods.ToListAsync();
            viewModel.FoodItems = this.CalendarDatabase.FoodItems
                .Where(fi => fi.CalendarDate.Date == date.Date && fi.Meal.MealID == mealId)
                .Include(f => f.SelectedFoodWeightType)
                .Include(f => f.Food).ThenInclude(fwt => fwt.FoodWeightTypes).ThenInclude(wt => wt.WeightType);

            var foodItems = viewModel.FoodItems.ToList();
            viewModel.TotalWeight = foodItems.Sum(f => f.Weight);
            
            viewModel.TotalEnergy = 
                foodItems.Where(f => f.SelectedFoodWeightType!=null).Sum(f => f.SelectedFoodWeightType.Weight * f.Weight);
            viewModel.TotalEnergy += foodItems.Where(f => f.SelectedFoodWeightType == null).Sum(f => f.Weight);


            //viewModel.TotalEnergy = foodItems.Sum(f => f.SelectedFoodWeightType.Weight * f.Weight);


            // create selectboxes
            foreach (var fi in viewModel.FoodItems)
            {
                var foodWeightTypes = new List<SelectListItem>();

                // Gram Weighttype
                bool isGramSelected = fi.SelectedFoodWeightType == null;
                var gram = new SelectListItem( "Gram", "1", isGramSelected);
                foodWeightTypes.Add(gram);

                foreach (var fwt in fi.Food.FoodWeightTypes)
                {
                    // add 
                    var item = new SelectListItem { Text = fwt.WeightType.UnitName, Value = fwt.WeightTypeId.ToString() };
                    if(fi.SelectedFoodWeightType != null && fi.SelectedFoodWeightType.FoodWeightTypeID == fwt.FoodWeightTypeID )
                    {
                        item.Selected = true;
                    }

                    foodWeightTypes.Add(item);
                }
                

                var calendarFoodItemViewModel = new CalendarFoodItemViewModel()
                {
                    FoodItem = fi,
                    FoodWeightTypes = foodWeightTypes
                };

                viewModel.FoodItemViewModels.Add(calendarFoodItemViewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create(int year, int month, int day, int foodId, int mealId)
        {
            var date = new DateTime(year, month, day);
            ViewData["Date"] = date;

            Food food = CalendarDatabase.Foods.Find(foodId);
            Meal meal = CalendarDatabase.Meals.Find(mealId);

            if (food == null || meal == null)
            {
                TempData["Error"] = "Wrong input";
                return Redirect(
                    string.Format("/calendarFood/add/{0}/{1}/{2}/{3}",
                    year, month, day, mealId));
            }

            // check if already exist and then update it
            CalendarFoodItem foodItem = this.CalendarDatabase.FoodItems.Where(cfi => cfi.CalendarDate.Date == date.Date && cfi.Meal.MealID == mealId && cfi.Food.FoodID == food.FoodID).SingleOrDefault();

            string GramUnitName = "gram";
            WeightType gram = CalendarDatabase.WeightTypes.Where(wt => wt.UnitName == GramUnitName).FirstOrDefault();

            if (foodItem == null)
            {
                foodItem = new CalendarFoodItem()
                {
                    Food = food,
                    Meal = meal,
                    Weight = decimal.ToInt32(food.Weight),                  
                    CalendarDate = date
                };

                //FoodWeightType fwt = new FoodWeightType()
                //{
                //    WeightTypeId = gram.WTID,
                //    WeightType = gram,
                //    FoodId = foodItem.Food.FoodID,
                //    Food = foodItem.Food,
                //    Weight = foodItem.Weight
                //};

                //foodItem.SelectedFoodWeightType = fwt;

              
                CalendarDatabase.FoodItems.Add(foodItem);
            } else
            {
                foodItem.Weight += decimal.ToInt32(food.Weight);
                foodItem.CalendarDate = date;
            }

            CalendarDatabase.SaveChanges();

            TempData["Success"] = "Created.";

            return Redirect(
                string.Format("/calendarFood/add/{0}/{1}/{2}/{3}",
                year, month, day, mealId));
        }

        [HttpDelete]        
        public IActionResult Delete(string back, int id)
        { 
            CalendarFoodItem foodItem = CalendarDatabase.FoodItems.Find(id);
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
            CalendarFoodItem foodItem = CalendarDatabase.FoodItems.Find(id);
            CalendarDatabase.FoodItems.Remove(foodItem);
            CalendarDatabase.SaveChanges();
            return Redirect("/CalendarFood/Add/" + back);
        }

        //[HttpPost]
        //public JsonResult HelloThorning(string search="")
        //{

        //    var FoodItems = this.CalendarDatabase.Foods.Where(f => f.Name.Contains(search));
        //    var data = new { status = "ok", result = FoodItems };
        //    return Json(data);
        //}



        [HttpPost]
        public IActionResult SearchForFoods([FromBody] SearchModel searchModel)
        {
            var allFoods = this.CalendarDatabase.Foods;
            var searchedFoodItems = allFoods.ToList();

            if (searchModel.SearchValue!="")
            {
                searchedFoodItems = allFoods.Where(f => f.Name.Contains(searchModel.SearchValue)).ToList();                
            }

            var calendarFoods = new ClientMadbordet.ViewModels.CalendarFoodsViewModel();
            calendarFoods.MealId = searchModel.MealId;
            calendarFoods.Foods = searchedFoodItems;
            calendarFoods.DateFormatted = searchModel.FormattedDate;

            return PartialView("_FoodsResults", calendarFoods);
        }

        [HttpPost]
        public IActionResult UpdateCalendarFoodWeight([FromBody] CalendarFoodUpdateModel calendarFoodUpdateModel)
        {
            var calendarFoodItem = this.CalendarDatabase.FoodItems
                    .Where(f => f.CalendarFoodItemID == calendarFoodUpdateModel.CalendarFoodItemId).First();
            var newWeight = calendarFoodUpdateModel.NewWeight;
            calendarFoodItem.Weight = newWeight;
            this.CalendarDatabase.SaveChanges();
            return Content(newWeight+""); 
        }
        
        [HttpPost]
        public IActionResult UpdateFoodItemWeightType([FromBody] FoodItemWeightTypeChangeViewModel responseValues)
        {
            var calendarFoodItem = this.CalendarDatabase.FoodItems
                    .Where(fi => fi.CalendarFoodItemID == responseValues.FoodItemId)
                    .Include(f => f.Food)
                    .FirstOrDefault();

            var newWeightTypeId = responseValues.WeightTypeId;
            var foodWeightType = this.CalendarDatabase.FoodWeightTypes
                .Where(fwt => fwt.WeightTypeId == responseValues.WeightTypeId && fwt.FoodId == calendarFoodItem.Food.FoodID)
                .Include(f => f.Food)
                .FirstOrDefault();

            var newWeight = responseValues.Pieces;
            if (foodWeightType != null)
            {
                calendarFoodItem.SelectedFoodWeightType = foodWeightType;
                newWeight = foodWeightType.Weight * responseValues.Pieces;
            } else
            {
                calendarFoodItem.SelectedFoodWeightType = null;
            }

            calendarFoodItem.Weight = newWeight;
            this.CalendarDatabase.SaveChanges();

            return Content(""); 
        }
    }



    
}