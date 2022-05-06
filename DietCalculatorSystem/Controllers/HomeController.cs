using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Models;
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
            var FoodOfTheDay = this.cache.Get<Food>(FOTDCacheKey);

            if (FoodOfTheDay == null)
            {
                var food = foods.GetRandomFood();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(24));

                this.cache.Set(FOTDCacheKey, food, cacheEntryOptions);
            }

            return View(this.cache.Get(FOTDCacheKey));
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
