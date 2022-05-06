using DietCalculatorSystem.Services.Foods;
using Microsoft.AspNetCore.Mvc;

namespace DietCalculatorSystem.Areas.Admin.Controllers
{
    public class FoodController : AdminController
    {
        private readonly IFoodService foods;

        public FoodController(IFoodService foods)
        {
            this.foods = foods;
        }

        public IActionResult Requests() 
        {
            var requestedFoods = foods
                .GetAllRequestedFoods();

            return View(requestedFoods);
        }

        public IActionResult Accept(string foodId)
        {
            foods.AcceptFood(foodId);

            return Redirect(nameof(Requests));
        }
        
        public IActionResult Deny(string foodId)
        {
            foods.RemoveFood(foodId);

            return Redirect(nameof(Requests));
        }
    }
}
