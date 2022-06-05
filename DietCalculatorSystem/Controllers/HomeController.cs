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
                bmiModel.BMIValueResult = "İdeal kilonuzun altındasınız. Az miktarda vücut yağına sahipsiniz. Eğer atletseniz bu istenebilir bir durumdur; fakat değilseniz zayıf VKİ seviyesi vücut ağırlığınızın düşük olduğunu gösterir ve bağışıklık sisteminizin zayıflamasına sebep olabilir. Eğer VKİ’niz ve vücut ağırlığınız düşükse kas hacminizi artırmak için sağlıklı bir beslenme ve egzersiz yoluyla kilo almaya çalışmalısınız. Size 'Surplus' diyeti öneririz.";
            }
            else if (bmiModel.BMIValue > 18.5 && bmiModel.BMIValue <= 24.9)
            {
                bmiModel.BMIValueResult = "İdeal vücut yağına sahipsiniz. Bu, uzun ve ciddi hastalık oranının en az olduğu bir hayat demektir. Aynı zamanda bu oran birçok insanın estetik olarak en çekici bulduğu orandır. Kilonuzu korumanızı öneririz. Bunu 'Balanced' diyet ile yapabilirsiniz.";
            }
            else if (bmiModel.BMIValue > 24.9 && bmiModel.BMIValue <= 29.9)
            {
                bmiModel.BMIValueResult = "İdeal kilonuzun üstündesiniz. Diyet ya da egzersizle kilo vermenin yollarını aramalısınız. Şu anki kilonuzla çeşitli hastalıklar için risk taşımaktasınız. Beslenme stilinizi değiştirerek ve egzersize daha fazla ağırlık vererek kilo vermelisiniz. Size 'Deficit' diyeti öneririz.";
            }
            else if (bmiModel.BMIValue > 29.9 && bmiModel.BMIValue <= 39.9)
            {
                bmiModel.BMIValueResult = "İdeal kilonuzun çok üstündesiniz. Obez kategorisindesiniz. Sağlıksız bir kilonuz var, bunun getirdiği ve getireceği sağlık sorunlarıyla karşı karşıyasınız. Beslenme stilinizi değiştirerek ve egzersize daha fazla ağırlık vererek kilo vermelisiniz. Size 'Deficit' diyeti öneririz.";
            }
            else
            {
                bmiModel.BMIValueResult = "İdeal kilonuzun çok üstündesiniz. Morbid obez kategorisindesiniz. Büyük risklerle karşı karşıyasınız ve bunlar ölümcül olabilir. Bu aşamada hastalık artık kronik ve tehlikeli bir hal almıştır. Beslenme stilinizi acilen değiştirmelisiniz. Size 'Deficit' diyeti öneririz.";
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
