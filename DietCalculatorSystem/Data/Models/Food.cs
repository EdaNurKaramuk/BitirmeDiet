using System;
using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Data.Models
{
    public class Food
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public double? Calories { get; set; }

        [Required]
        public double? Proteins { get; set; }

        [Required]
        public double? Fats { get; set; }

        [Required]
        public double? Carbohydrates { get; set; }

        public bool IsPublic { get; set; }
    }
}
