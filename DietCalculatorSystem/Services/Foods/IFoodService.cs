using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Models.Foods;
using DietCalculatorSystem.Services.Foods.Models;
using System.Collections.Generic;

namespace DietCalculatorSystem.Services.Foods
{
    public interface IFoodService
    {
        Food GetFood(string id);
        FoodQueryServiceModel All(int foodsPerPage = 5,
            int currentPage = 1,
            string searchTerm = null,
            FoodSorting foodSorting = FoodSorting.Calories);

        bool FoodExists(string foodName);

        void CreateFood(string name,
            double? calories,
            double? proteins,
            double? fats,
            double? carbohydrates,
            string description,
            string pictureUrl);

        FoodDetailsServiceModel GetDetails(string foodId);

        void RemoveFood(string foodId);

        Food GetRandomFood();

        List<Food> GetAllRequestedFoods();

        void AcceptFood(string foodId);
    }
}
