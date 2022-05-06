using DietCalculatorSystem.Models.Diets;
using DietCalculatorSystem.Services.Diets;
using DietCalculatorSystem.Services.Diets.Models;
using DietCalculatorSystem.Services.Foods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DietCalculatorSystem.Controllers
{
    
    public class DietController : Controller
    {
        private const int foodsPerPage = 4;

        private readonly IFoodService foods;
        private readonly IDietService diets;

        public DietController(IFoodService foods,
            IDietService diets)
        {
            this.foods = foods;
            this.diets = diets;
        }

        [Authorize]
        public IActionResult Surplus([FromQuery] DietFormModel query)
        {
            query.DietId = diets.GetDietId(this.User.Identity.Name, nameof(Surplus));

            var diet = diets.GetAllMeals(query.DietId);

            query.BreakfastFoods = diet.BreakfastFoods;
            query.LunchFoods = diet.LunchFoods;
            query.DinnerFoods = diet.DinnerFoods;

            CalculateNutritions(query,diet);

            var queryResults = this.foods.All(
                foodsPerPage,
                query.CurrentPage,
                query.SearchTerm,
                query.Sorting);

            query.Foods = queryResults.Foods;
            query.TotalFoods = queryResults.TotalFoods;

            return View(query);
        }

        [Authorize]
        public IActionResult Deficit([FromQuery] DietFormModel query)
        {
            query.DietId = diets.GetDietId(this.User.Identity.Name, nameof(Deficit));

            var diet = diets.GetAllMeals(query.DietId);

            query.BreakfastFoods = diet.BreakfastFoods;
            query.LunchFoods = diet.LunchFoods;
            query.DinnerFoods = diet.DinnerFoods;

            CalculateNutritions(query, diet);

            var queryResults = this.foods.All(
                foodsPerPage,
                query.CurrentPage,
                query.SearchTerm,
                query.Sorting);

            query.Foods = queryResults.Foods;
            query.TotalFoods = queryResults.TotalFoods;

            return View(query);
        }

        [Authorize]
        public IActionResult Balanced([FromQuery] DietFormModel query)
        {
            query.DietId = diets.GetDietId(this.User.Identity.Name, nameof(Balanced));

            var diet = diets.GetAllMeals(query.DietId);

            query.BreakfastFoods = diet.BreakfastFoods;
            query.LunchFoods = diet.LunchFoods;
            query.DinnerFoods = diet.DinnerFoods;

            CalculateNutritions(query, diet);

            var queryResults = this.foods.All(
                foodsPerPage,
                query.CurrentPage,
                query.SearchTerm,
                query.Sorting);

            query.Foods = queryResults.Foods;
            query.TotalFoods = queryResults.TotalFoods;

            return View(query);
        }

        [Authorize]
        public IActionResult AddBreakfast(string foodId, string dietId, string dietType)
        {
            diets.AddBreakfastFood(foodId, dietId);

            return Redirect($"/Diet/{dietType}");
        }

        [Authorize]
        public IActionResult AddLunch(string foodId, string dietId, string dietType)
        {
            diets.AddLunchFood(foodId, dietId);

            return Redirect($"/Diet/{dietType}");
        }

        [Authorize]
        public IActionResult AddDinner(string foodId, string dietId, string dietType)
        {
            diets.AddDinnerFood(foodId, dietId);

            return Redirect($"/Diet/{dietType}");
        }

        [Authorize]
        public IActionResult RemoveBreakfast(string foodId, string dietId, string dietType) 
        {
            diets.RemoveBreakFastFood(foodId, dietId);

            return Redirect($"/Diet/{dietType}");
        }

        [Authorize]
        public IActionResult RemoveLunch(string foodId, string dietId, string dietType) 
        {
            diets.RemoveLunchFood(foodId, dietId);

            return Redirect($"/Diet/{dietType}");
        }

        [Authorize]
        public IActionResult RemoveDinner(string foodId, string dietId, string dietType) 
        {
            diets.RemoveDinnerFood(foodId,dietId);

            return Redirect($"/Diet/{dietType}");
        }

        private static void CalculateNutritions(DietFormModel query, DietServiceModel diet)
        {
            var consumedCalories =
                query.BreakfastFoods.Sum(x => x.Calories * x.Quantity) +
                query.LunchFoods.Sum(x => x.Calories * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Calories * x.Quantity);

            var consumedProteins = query.BreakfastFoods.Sum(x => x.Proteins * x.Quantity) +
                query.LunchFoods.Sum(x => x.Proteins * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Proteins * x.Quantity);

            var consumedFats = query.BreakfastFoods.Sum(x => x.Fats * x.Quantity) +
                query.LunchFoods.Sum(x => x.Fats * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Fats * x.Quantity);

            var consumedCarbohydrates = query.BreakfastFoods.Sum(x => x.Carbohydrates * x.Quantity) +
                query.LunchFoods.Sum(x => x.Carbohydrates * x.Quantity) +
                query.DinnerFoods.Sum(x => x.Carbohydrates * x.Quantity);

            query.TotalCalories = Math.Round((double)(diet.TotalCalories - consumedCalories), 2);
            query.TotalProteins = Math.Round((double)(diet.TotalProteins - consumedProteins), 2);
            query.TotalFats = Math.Round((double)(diet.TotalFats - consumedFats), 2);
            query.TotalCarbohydrates = Math.Round((double)(diet.TotalCarbohydrates - consumedCarbohydrates), 2);
        }
    }
}
