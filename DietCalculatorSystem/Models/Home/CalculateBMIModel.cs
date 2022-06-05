using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static DietCalculatorSystem.Data.DataConstants.BMICalculator;

namespace DietCalculatorSystem.Models.Home
{
    public class CalculateBMIModel
    {
        [Required(ErrorMessage = "Boy zorunlu.")]
        [Range(MinHeight, MaxHeight, ErrorMessage = "Boy en az {1} ve en fazla {2} cm olmalı.")]
        public double? Height { get; set; }

        [Required(ErrorMessage = "Kilo zorunlu.")]
        [Range(MinWeight, MaxWeight, ErrorMessage = "Kilo en az {1} ve en fazla {2} cm olmalı.")]
        public double? Weight { get; set; }

        public double? BMIValue { get; set; }

        public string BMIValueResult { get; set; }
    }
}
