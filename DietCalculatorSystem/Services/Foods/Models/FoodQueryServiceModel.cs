using System.Collections.Generic;

namespace DietCalculatorSystem.Services.Foods.Models
{
    public class FoodQueryServiceModel
    {
        public int FoodsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalFoods { get; set; }

        public IEnumerable<FoodServiceModel> Foods { get; set; }
    }
}
