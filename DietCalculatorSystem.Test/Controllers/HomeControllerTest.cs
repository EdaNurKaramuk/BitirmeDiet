using DietCalculatorSystem.Controllers;
using DietCalculatorSystem.Data.Models;
using MyTested.AspNetCore.Mvc;
using Xunit;
using System;

using static DietCalculatorSystem.WebConstants.Cache;
using static DietCalculatorSystem.Test.Data.Foods;

namespace DietCalculatorSystem.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void LoggedOutUserReturnDefaultView()
            => MyController<HomeController>
            .Calling(c => c
                .Index())
            .ShouldReturn()
            .View();
        
        
        [Fact]
        public void LoggedInUserShouldRedirectToIndexLoggedIn()
            => MyController<HomeController>
            .Instance(instance => instance
                .WithUser())
            .Calling(c => c
                .Index())
            .ShouldReturn()
            .Redirect(r => r
                .To<HomeController>(c => c
                    .IndexLoggedIn()));




        [Fact]
        public void LoggedInUserReturnDefaultView()
            => MyController<HomeController>
            .Instance(instance => instance
                .WithData(TenPublicFoods))
            .Calling(c => c
                .IndexLoggedIn())
            .ShouldHave()
            .MemoryCache(cache => cache
                .ContainingEntry(entry => entry
                    .WithKey(FOTDCacheKey)
                    .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromHours(24))
                    .WithValueOfType<Food>()))
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .View();


        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
            .Calling(c => c
                .Error())
            .ShouldReturn()
            .View();
    }
}
