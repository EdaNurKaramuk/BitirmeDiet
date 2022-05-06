using DietCalculatorSystem.Data.Models.ManyToManyRelationships;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Data.Models
{
    public class Diet
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        //Total
        public double? TotalCalories { get; set; }
        public double? TotalProteins { get; set; }
        public double? TotalFats { get; set; }
        public double? TotalCarbohydrates { get; set; }
        
        public IEnumerable<TotalFood> TotalFoods { get; set; } = new List<TotalFood>();

        //Breakfast
        public double? BreakfastCalories { get; set; }
        public double? BreakfastProteins { get; set; }
        public double? BreakfastFats { get; set; }
        public double? BreakfastCarbohydrates { get; set; }
        public ICollection<BreakfastFood> BreakfastFoods { get; set; } = new List<BreakfastFood>();

        //Lunch
        public double? LunchCalories { get; set; }
        public double? LunchProteins { get; set; }
        public double? LunchFats { get; set; }
        public double? LunchCarbohydrates { get; set; }
        public ICollection<LunchFood> LunchFoods { get; set; } = new List<LunchFood>();

        //Dinner
        public double? DinnerCalories { get; set; }
        public double? DinnerProteins { get; set; }
        public double? DinnerFats { get; set; }
        public double? DinnerCarbohydrates { get; set; }
        public ICollection<DinnerFood> DinnerFoods { get; set; } = new List<DinnerFood>();

    }
}
