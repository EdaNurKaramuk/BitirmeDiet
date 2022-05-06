using DietCalculatorSystem.Areas.Admin.Controllers;
using DietCalculatorSystem.Data.Models;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

using static DietCalculatorSystem.Test.Data.Foods;

namespace DietCalculatorSystem.Test.Controllers
{
    public class AdminFoodControllerTest
    {
        [Fact]
        public void RequestsShouldReturnCorrectView()
            => MyController<FoodController>
            .Instance()
            .Calling(c => c
                .Requests())
            .ShouldReturn()
            .View(v => v
                .WithModelOfType<List<Food>>());

        [Fact]
        public void AcceptShouldWorkSuccessfullyAndRedirectToRequestsView()
            => MyController<FoodController>
            .Instance(i => i
                .WithData(RequestedFood))
            .Calling(c => c
                .Accept("1"))
            .ShouldReturn()
            .Redirect();
            
            [Fact]
        public void DenyShouldWorkSuccessfullyAndRedirectToRequestsView()
            => MyController<FoodController>
            .Instance(i => i
                .WithData(RequestedFood))
            .Calling(c => c
                .Deny("1"))
            .ShouldReturn()
            .Redirect();
    }
}
