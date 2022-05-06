using System.ComponentModel.DataAnnotations;

using static DietCalculatorSystem.Data.DataConstants.Calculator;

namespace DietCalculatorSystem.Models.Home
{
    public class CalculateCaloriesFormModel
    {
        [Required(ErrorMessage = "Age is required.")]
        [Range(MinAge,MaxAge)]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Height is required.")]
        [Range(MinHeight,MaxHeight)]
        public double? Height { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        [Range(MinWeight,MaxWeight)]
        public double? Weight { get; set; }

        [Required(ErrorMessage = "Activity is required.")]
        public double? Activity { get; set; }
    }
}
