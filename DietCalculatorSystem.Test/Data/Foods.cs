using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Data.Models.ManyToManyRelationships;
using System.Collections.Generic;
using System.Linq;

using static DietCalculatorSystem.Test.Data.Diets;

namespace DietCalculatorSystem.Test.Data
{
    public static class Foods
    {
        public static IEnumerable<Food> TenPublicFoods
            => Enumerable.Range(0, 10).Select(i => new Food
            {
                IsPublic = true
            });
        
        public static Food FirstFood
            => new Food
            {
                Id = "1",
                IsPublic = true
            };

        public static Food SecondFood
            => new Food
            {
                Id = "2",
                IsPublic = true
            };

        public static Food ThirdFood
            => new Food
            {
                Id = "3",
                IsPublic = true
            };

        public static Food RequestedFood
           => new Food
           {
               Id = "1",
               IsPublic = false
           };

        public static BreakfastFood breakfastFood
            => new BreakfastFood
            {
                Diet = FirstDiet,
                Food = FirstFood,
            };
        
        public static DinnerFood dinnerFood
            => new DinnerFood
            {
                Diet = FirstDiet,
                Food = FirstFood,
            };
        
        public static LunchFood lunchFood
            => new LunchFood
            {
                Diet = FirstDiet,
                Food = FirstFood,
            };

    }
}
