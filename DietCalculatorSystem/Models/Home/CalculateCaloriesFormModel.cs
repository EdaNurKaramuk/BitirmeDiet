using System.ComponentModel.DataAnnotations;

using static DietCalculatorSystem.Data.DataConstants.Calculator;

namespace DietCalculatorSystem.Models.Home
{
    public class CalculateCaloriesFormModel
    {
        [Required(ErrorMessage = "Yaş zorunlu.")]
        [Range(MinAge,MaxAge, ErrorMessage = "Yaş en küçük 15 ve en çok 90 olmalı.")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Cinsiyet zorunlu.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Boy zorunlu.")]
        [Range(MinHeight, MaxHeight, ErrorMessage = "Boy en az 100 ve en fazla 300 cm olmalı.")]
        public double? Height { get; set; }

        [Required(ErrorMessage = "Kilo zorunlu.")]
        [Range(MinWeight, MaxWeight, ErrorMessage = "Boy en az 100 ve en fazla 300 cm olmalı.")]
        public double? Weight { get; set; }

        [Required(ErrorMessage = "Aktivite zorunlu.")]
        public double? Activity { get; set; }
    }
}
