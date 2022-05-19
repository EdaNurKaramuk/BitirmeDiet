using DietCalculatorSystem.Services.Diets.Models;

namespace DietCalculatorSystem.Services.Diets
{
    public interface IDietService
    {
        string GetDietId(string name,
            string methodName);

        void CalculateDiet(string name,
            int? age,
            string gender,
            double? height,
            double? weight,
            double? activity);

        DietServiceModel GetAllMeals(string dietId);

        void AddBreakfastFood(string foodId,
            string dietId);

        void AddLunchFood(string foodId,
            string dietId);

        void AddDinnerFood(string foodId,
            string dietId);

        void RemoveBreakFastFood(string foodId,
            string dietId);

        void RemoveLunchFood(string foodId,
            string dietId);

        void RemoveDinnerFood(string foodId,
            string dietId);
    }
}
