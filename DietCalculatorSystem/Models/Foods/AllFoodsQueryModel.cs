using DietCalculatorSystem.Services.Foods.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Models.Foods
{
    public class AllFoodsQueryModel
    {
        public const int FoodsPerPage = 8;

        public int CurrentPage { get; set; } = 1;

        public int TotalFoods { get; set; }

        public FoodSorting Sorting { get; set; }

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public IEnumerable<FoodServiceModel> Foods { get; set; }
    }
}
