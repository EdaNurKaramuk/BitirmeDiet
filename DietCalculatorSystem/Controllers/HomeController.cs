using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Models;
using DietCalculatorSystem.Models.Home;
using DietCalculatorSystem.Services.Foods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Diagnostics;

using static DietCalculatorSystem.WebConstants.Cache;

namespace DietCalculatorSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFoodService foods;
        private readonly IMemoryCache cache;
        public HomeController(IFoodService foods,
            IMemoryCache cache)
        {
            this.foods = foods;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(IndexLoggedIn));
            }

            return View();
        }

        [Authorize]
        public IActionResult IndexLoggedIn()
        {
            //return View(new CalculateBMIModel());

            return View(this.cache.Get(FOTDCacheKey));
        }

        [Authorize]
        [HttpPost]
        public IActionResult IndexLoggedIn(CalculateBMIModel bmiModel)
        {
            double? heightMeter = bmiModel.Height / 100.0;
            bmiModel.BMIValue = bmiModel.Weight / (Math.Pow((double)heightMeter, 2));

            //Değer aralığına göre ideal kilonun altı-üstü bilgilerini burada tanımladım.
            if(bmiModel.BMIValue <= 18.5)
            {
                bmiModel.BMIValueResult = "İdeal kilonun altında.";
            }
            else if (bmiModel.BMIValue > 18.5 && bmiModel.BMIValue <= 24.9)
            {
                bmiModel.BMIValueResult = "İdeal kiloda.";
            }
            else if (bmiModel.BMIValue > 24.9 && bmiModel.BMIValue <= 29.9)
            {
                bmiModel.BMIValueResult = "İdeal kilonun üstünde.";
            }
            else if (bmiModel.BMIValue > 29.9 && bmiModel.BMIValue <= 39.9)
            {
                bmiModel.BMIValueResult = "İdeal kilonun çok üstünde (Obez).";
            }
            else
            {
                bmiModel.BMIValueResult = "İdeal kilonun çok üstünde (Morbid Obez).";
            }

            return View(bmiModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
