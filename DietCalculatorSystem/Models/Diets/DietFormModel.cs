using DietCalculatorSystem.Models.Foods;
using DietCalculatorSystem.Services.Foods.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Models.Diets
{
    public class DietFormModel
    {
        public string DietId { get; set; }
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

        //Search
        public const int FoodsPerPage = 5;

        public int CurrentPage { get; set; } = 1;

        public int TotalFoods { get; set; }

        public FoodSorting Sorting { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public IEnumerable<FoodServiceModel> Foods { get; set; }
    }
}
