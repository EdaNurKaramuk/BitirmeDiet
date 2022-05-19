using DietCalculatorSystem.Models.Home;
using DietCalculatorSystem.Services.Diets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DietCalculatorSystem.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly IDietService diets;

        public CalculatorController(IDietService diets)
        {
            this.diets = diets;
        }

        [Authorize]
        public IActionResult Calculator()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Calculator(CalculateCaloriesFormModel calculateModel)
        {
            bool isModelValid = ValidateActivity(calculateModel.Activity) && ValidateGender(calculateModel.Gender);

            if (!isModelValid)
            {
                ModelState.AddModelError(nameof(calculateModel.Activity), "Invalid activity type");
            }

            if (!ModelState.IsValid)
            {
                return View(calculateModel);
            }

            diets.CalculateDiet(this.User.Identity.Name,
                calculateModel.Age,
                calculateModel.Gender,
                calculateModel.Height,
                calculateModel.Weight,
                calculateModel.Activity);

            return Redirect("/Food/All");
        }

        private static bool ValidateGender(string gender)
        {
            return gender switch
            {
                "Male" => true,
                "Female" => true,
                _ => false
            };
        }

        private static bool ValidateActivity(double? activity)
        {
            return activity switch
            {
                1.2 => true,
                1.375 => true,
                1.55 => true,
                1.725 => true,
                _ => false
            };
        }
    }
}
