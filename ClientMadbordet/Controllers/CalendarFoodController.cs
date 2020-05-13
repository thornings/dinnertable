using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ClientMadbordet.Models;
using ClientMadbordet.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace ClientMadbordet.Controllers
{
    public class CalendarFoodController : Controller
    {
        private CalendarContext _calendarDatabase;
        private readonly CalendarFoodRepository _calendarFoodRepository;
        private readonly CalendarFoodLogic _calendarFoodLogic;

        public CalendarFoodController(CalendarContext db)
        {
            _calendarDatabase = db;
            _calendarFoodRepository = new CalendarFoodRepository(db);
            _calendarFoodLogic = new CalendarFoodLogic();
        }

        [HttpGet]
        public IActionResult Add(int year, string month, int day, int mealId)
        {
            CalendarFoodViewModel viewModel = new CalendarFoodViewModel
            {
                Meal = _calendarFoodRepository.FindMealById(mealId)
            };

            if (viewModel.Meal is null)
            {
                return RedirectToAction("/");
            }

            /* test */
            //CalendarFoodItem fii = _calendarDatabase.FoodItems
            //.Where(fi => fi.CalendarFoodItemID == 144).Include(f => f.SelectedFoodWeightType).FirstOrDefault();
            ////            _calendarDatabase.Remove(fii.SelectedFoodWeightType);
            //fii.SelectedFoodWeightType = null;
            //_calendarDatabase.SaveChanges();


            if (DateTime.TryParseExact(year + "-" + month + "-" + day, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime date))
            {
                viewModel.DateFormatted = date.Year + "/" + date.Month.ToString("D2") + "/" + date.Day;
                viewModel.Date = date;
                viewModel.Foods = _calendarDatabase.Foods.ToList();
                viewModel.FoodItems = _calendarFoodRepository.GetFoodItemsByDateAndMeal(date.Date, mealId);

                var tempFoodItems = viewModel.FoodItems.ToList();
                viewModel.TotalWeight = _calendarFoodLogic.FoodItemsTotalWeight(tempFoodItems);
                viewModel.TotalEnergy = _calendarFoodLogic.FoodItemsTotalEnergy(tempFoodItems);

                // create selectboxes
                foreach (var fi in viewModel.FoodItems)
                {
                    List<SelectListItem> foodWeightTypeSelectedListItems = _calendarFoodLogic.CreateSelectedListItems(fi);

                    var calendarFoodItemViewModel = new CalendarFoodItemViewModel()
                    {
                        FoodItem = fi,
                        FoodWeightTypes = foodWeightTypeSelectedListItems
                    };

                    viewModel.FoodItemViewModels.Add(calendarFoodItemViewModel);
                }
            }
            else
            {
                return RedirectToAction("/calendar");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create(int year, string month, int day, int foodId, int mealId)
        {
            // TODO PARSE TRY
            var date = new DateTime(year, int.Parse(month), day);
            ViewData["Date"] = date;

            Food food = _calendarDatabase.Foods.Find(foodId);
            Meal meal = _calendarDatabase.Meals.Find(mealId);

            if (food == null || meal == null)
            {
                TempData["Error"] = "Wrong input";
                return Redirect(
                    string.Format("/calendarFood/add/{0}/{1}/{2}/{3}",
                    year, month, day, mealId));
            }

            // check if already exist and then update it
            CalendarFoodItem foodItem = _calendarDatabase.FoodItems.Where(cfi => cfi.CalendarDate.Date == date.Date && cfi.Meal.MealID == mealId && cfi.Food.FoodID == food.FoodID).SingleOrDefault();

            string GramUnitName = "gram";
            WeightType gram = _calendarDatabase.WeightTypes.Where(wt => wt.UnitName == GramUnitName).FirstOrDefault();
            
            if (foodItem == null)
            {
                foodItem = new CalendarFoodItem()
                {
                    Food = food,
                    Meal = meal,
                    Weight = decimal.ToInt32(food.Weight),                  
                    CalendarDate = date
                };

                _calendarDatabase.FoodItems.Add(foodItem);
            } else
            {
                foodItem.Weight += decimal.ToInt32(food.Weight);
            }

            _calendarDatabase.SaveChanges();

            TempData["Success"] = "Created.";

            return Redirect(
                string.Format("/calendarFood/add/{0}/{1}/{2}/{3}",
                year, month, day, mealId));
        }

        [HttpDelete]        
        public IActionResult Delete(string back, int id)
        { 
            CalendarFoodItem foodItem = _calendarDatabase.FoodItems.Find(id);
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
            CalendarFoodItem foodItem = _calendarDatabase.FoodItems.Find(id);
            _calendarDatabase.FoodItems.Remove(foodItem);
            _calendarDatabase.SaveChanges();
            return Redirect("/CalendarFood/Add/" + back);
        }

        [HttpPost]
        public IActionResult SearchForFoods([FromBody] SearchModel searchModel)
        {
            var allFoods = _calendarDatabase.Foods;
            var searchedFoodItems = allFoods.ToList();

            if (searchModel.SearchValue!="")
            {
                searchedFoodItems = allFoods.Where(f => f.Name.Contains(searchModel.SearchValue)).ToList();                
            }

            var calendarFoods = new ClientMadbordet.ViewModels.CalendarFoodsViewModel
            {
                MealId = searchModel.MealId,
                Foods = searchedFoodItems,
                DateFormatted = searchModel.FormattedDate
            };

            return PartialView("_FoodsResults", calendarFoods);
        }

        [HttpPost]
        public IActionResult UpdateCalendarFoodWeight([FromBody] CalendarFoodUpdateModel calendarFoodUpdateModel)
        {
            var calendarFoodItem = _calendarDatabase.FoodItems
                    .Where(f => f.CalendarFoodItemID == calendarFoodUpdateModel.CalendarFoodItemId).First();
            var newWeight = calendarFoodUpdateModel.NewWeight;
            calendarFoodItem.Weight = newWeight;
            _calendarDatabase.SaveChanges();
            return Content(newWeight+""); 
        }
        
        [HttpPost]
        public IActionResult UpdateFoodItemWeightType([FromBody] FoodItemWeightTypeChangeViewModel responseValues)
        {
            var calendarFoodItem = _calendarDatabase.FoodItems
                    .Where(fi => fi.CalendarFoodItemID == responseValues.FoodItemId)
                    .Include(f => f.Food)
                    .Include(f => f.SelectedFoodWeightType)
                    .FirstOrDefault();

            var newWeightTypeId = responseValues.WeightTypeId;
            var foodWeightType = _calendarDatabase.FoodWeightTypes
                .Where(fwt => fwt.WeightTypeId == responseValues.WeightTypeId && fwt.FoodId == calendarFoodItem.Food.FoodID)
                .Include(f => f.Food)
                .FirstOrDefault();

            if (foodWeightType != null)
            {
                calendarFoodItem.SelectedFoodWeightType = foodWeightType;
            } else
            {
                calendarFoodItem.SelectedFoodWeightType = null;
            }
            _calendarDatabase.SaveChanges();



            return Content(""); 
        }
    }
    
}