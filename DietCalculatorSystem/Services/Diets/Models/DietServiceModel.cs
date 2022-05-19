using DietCalculatorSystem.Services.Foods.Models;
using System.Collections.Generic;

namespace DietCalculatorSystem.Services.Diets.Models
{
    public class DietServiceModel
    {
        public double? TotalCalories { get; set; }
        public double? TotalProteins { get; set; }
        public double? TotalFats { get; set; }
        public double? TotalCarbohydrates { get; set; }

        //Breakfast
        public ICollection<FoodServiceModel> BreakfastFoods { get; set; } = new List<FoodServiceModel>();

        //Lunch
        public ICollection<FoodServiceModel> LunchFoods { get; set; } = new List<FoodServiceModel>();

        //Dinner
        public ICollection<FoodServiceModel> DinnerFoods { get; set; } = new List<FoodServiceModel>();

    }
}
