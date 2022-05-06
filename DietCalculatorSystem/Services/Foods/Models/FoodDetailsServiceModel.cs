using DietCalculatorSystem.Data.Models;

namespace DietCalculatorSystem.Services.Foods.Models
{
    public class FoodDetailsServiceModel
    {
        public Food MainFood { get; set; }
        public Food FirstSuggestedFood { get; set; }
        public Food SecondSuggestedFood { get; set; }
    }
}
