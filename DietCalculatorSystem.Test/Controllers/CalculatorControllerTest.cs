using DietCalculatorSystem.Controllers;
using DietCalculatorSystem.Models.Foods;
using DietCalculatorSystem.Models.Home;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static DietCalculatorSystem.Test.Data.Users;

namespace DietCalculatorSystem.Test.Controllers
{
    public class CalculatorControllerTest
    {
        [Fact]
        public void CalculatorShouldReturnCorrectView()
            => MyController<CalculatorController>
            .Instance()
            .Calling(c => c
                .Calculator())
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();
        
        [Fact]
        public void CalculatorShouldWorkCorrectlyAndRedirectToCorrectView()
            => MyController<CalculatorController>
            .Instance(instance => instance
                .WithData(GetUser)
                .WithUser("Testt"))
            .Calling(c => c
                .Calculator(new CalculateCaloriesFormModel() 
                {
                    Gender = "Male",
                    Activity = 1.2,
                    Age = 19,
                    Height = 190,
                    Weight = 90
                }))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<FoodController>(c => c
                    .All(With
                        .Any<AllFoodsQueryModel>())));

        [Theory]
        [InlineData("Male", 15, 19, 190, 90)]
        [InlineData("Gay", 1.2, 19, 190, 90)]
        [InlineData("Male", 1.2, 0, 190, 90)]
        [InlineData("Male", 1.2, 19, 0, 90)]
        [InlineData("Male", 1.2, 19, 190, 0)]
        public void CalculatorShouldThrowExceptionAndRedirectToCorrectView(string gender,
            double? activity,
            int? age,
            double? height,
            double? weight)
            => MyController<CalculatorController>
            .Instance()
            .Calling(c => c
                .Calculator(new CalculateCaloriesFormModel()
                {
                    Gender = gender,
                    Activity = activity,
                    Age = age,
                    Height = height,
                    Weight = weight
                }))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
            .AndAlso()
            .ShouldHave()
            .InvalidModelState(withNumberOfErrors: 1)
            .AndAlso()
            .ShouldReturn()
            .View();
    }
}
