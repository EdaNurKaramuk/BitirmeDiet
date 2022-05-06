using DietCalculatorSystem.Controllers;
using DietCalculatorSystem.Models.Foods;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static DietCalculatorSystem.Test.Data.Foods;
using static DietCalculatorSystem.WebConstants.AdminConstants;

namespace DietCalculatorSystem.Test.Controllers
{
    public class FoodControllerTest
    {
        [Fact]
        public void AllShouldReturnView()
            => MyController<FoodController>
            .Instance()
            .Calling(c => c
                .All(new AllFoodsQueryModel()))
            .ShouldReturn()
            .View(c => c
                .WithModelOfType<AllFoodsQueryModel>());

        [Fact]
        public void AddShouldReturnView()
            => MyController<FoodController>
            .Instance()
            .Calling(c => c
                .Add())
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void AddFoodShouldWorkAndRedirectToTheCorrectView()
            => MyController<FoodController>
            .Instance()
            .Calling(c => c
                .Add(new AddFoodFormModel()
                {
                    Name = "Carrot",
                    Description = "TestingRandomCarrotDescription",
                    Calories = 10,
                    Proteins = 10,
                    Fats = 10,
                    Carbohydrates = 10,
                    PictureUrl = "https://if-koubou.com/img/images/what-is-a-url-uniform-resource-locator_4.png"
                }))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<FoodController>(c => c
                    .All(With
                        .Any<AllFoodsQueryModel>())));


        [Theory]
        [InlineData("Carrot", "1", 10, 10, 10, 10, "https://if-koubou.com/img/images/what-is-a-url-uniform-resource-locator_4.png")]
        [InlineData("1", "TestingRandomCarrotDescription", 10, 10, 10, 10, "https://if-koubou.com/img/images/what-is-a-url-uniform-resource-locator_4.png")]
        [InlineData("Carrot", "TestingRandomCarrotDescription", 10, 10, 10, 10, "1")]
        [InlineData("Carrot", "TestingRandomCarrotDescription", null, 10, 10, 10, "https://if-koubou.com/img/images/what-is-a-url-uniform-resource-locator_4.png")]
        public void AddFoodShouldThrewNameError(string name,
            string description,
            double? calories,
            double? proteins,
            double? fats,
            double? carbs,
            string pictureUrl)
            => MyController<FoodController>
            .Instance()
            .Calling(c => c
                        .Add(new AddFoodFormModel()
                        {
                            Name = name,
                            Description = description,
                            Calories = calories,
                            Proteins = proteins,
                            Fats = fats,
                            Carbohydrates = carbs,
                            PictureUrl = pictureUrl
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

        [Fact]
        public void DetailsShouldReturnCorrectView()
            => MyController<FoodController>
            .Instance(instance => instance
                .WithData(FirstFood, SecondFood, ThirdFood))
            .Calling(c => c
                .Details("1"))
            .ShouldReturn()
            .View();

        [Fact]
        public void DeleteShouldRemoveSuccessfullyAndReturnCorrectView()
            => MyController<FoodController>
            .Instance(instance => instance
                .WithData(FirstFood)
                .WithUser(u => u
                    .InRole(AdministratorRoleName)))
            .Calling(c => c
                .Delete("1"))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests(AdministratorRoleName))
            .ValidModelState()
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<FoodController>(c => c
                    .All(With
                        .Any<AllFoodsQueryModel>())));
    }
}
