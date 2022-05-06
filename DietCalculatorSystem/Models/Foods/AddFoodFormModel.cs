using System.ComponentModel.DataAnnotations;

using static DietCalculatorSystem.Data.DataConstants.Food;

namespace DietCalculatorSystem.Models.Foods
{

    public class AddFoodFormModel
    {
        [Required]
        [StringLength(MaxFoodNameLength, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.", MinimumLength = MinFoodNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(MaxDescriptionLength, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.", MinimumLength = MinDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [Url]
        [Display(Name = "Picture Url")]
        public string PictureUrl { get; set; }

        [Required]
        [RegularExpression(ValidNumberRegex, ErrorMessage = "Invalid number! It should be separeted with a '.' symbol instead of ','!")]
        [Range(MinCalories, MaxCalories)]
        public double? Calories { get; set; }

        [Required]
        [RegularExpression(ValidNumberRegex, ErrorMessage = "Invalid number! It should be separeted with a '.' symbol instead of ','!")]
        [Range(MinProteins, MaxProteins)]
        public double? Proteins { get; set; }

        [Required]
        [RegularExpression(ValidNumberRegex, ErrorMessage = "Invalid number! It should be separeted with a '.' symbol instead of ','!")]
        [Range(MinFats, MaxFats)]
        public double? Fats { get; set; }

        [Required]
        [RegularExpression(ValidNumberRegex, ErrorMessage = "Invalid number! It should be separeted with a '.' symbol instead of ','!")]
        [Range(MinCarbohydrates, MaxCarbohydrates)]
        public double? Carbohydrates { get; set; }
    }
}
