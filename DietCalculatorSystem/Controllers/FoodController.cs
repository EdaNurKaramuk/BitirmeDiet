using DietCalculatorSystem.Models.Foods;
using DietCalculatorSystem.Services.Foods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static DietCalculatorSystem.WebConstants.AdminConstants;

namespace DietCalculatorSystem.Controllers
{
    public class FoodController : Controller
    {
        private const int foodsPerPage = 8;

        private readonly IFoodService foods;

        public FoodController(IFoodService foods)
        {
            this.foods = foods;
        }

        public IActionResult All([FromQuery] AllFoodsQueryModel query)
        {
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
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddFoodFormModel foodModel)
        {
            if (foods.FoodExists(foodModel.Name))
            {
                this.ModelState.AddModelError(nameof(foodModel.Name), "Food already exists!");
            }

            if (!ModelState.IsValid)
            {
                return View(foodModel);
            }

            foods.CreateFood(foodModel.Name,
                foodModel.Calories,
                foodModel.Proteins,
                foodModel.Fats,
                foodModel.Carbohydrates,
                foodModel.Description,
                foodModel.PictureUrl);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(string foodId) 
        {
            return this.View(foods.GetDetails(foodId));
        }

        [Authorize(Roles = AdministratorRoleName)]
        public IActionResult Delete(string foodId)
        {
            foods.RemoveFood(foodId);

            return RedirectToAction(nameof(All));
        }
    }
}
