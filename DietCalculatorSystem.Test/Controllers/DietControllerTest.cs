using DietCalculatorSystem.Controllers;
using DietCalculatorSystem.Models.Diets;
using MyTested.AspNetCore.Mvc;
using Xunit;

using static DietCalculatorSystem.Test.Data.Users;
using static DietCalculatorSystem.Test.Data.Foods;
using static DietCalculatorSystem.Test.Data.Diets;

namespace DietCalculatorSystem.Test.Controllers
{
    public class DietControllerTest
    {
        //Deficit

        [Fact]
        public void DeficitShouldReturnCorrectView()
            => MyController<DietController>
            .Instance(instance => instance
                .WithData(GetUser)
                .WithUser("Testt"))
            .Calling(c => c
                .Deficit(new DietFormModel()))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void AddBreakfastShouldAddSuccessfullyAndRedirectToDeficitView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(FirstFood, FirstDiet))
            .Calling(c => c
                .AddBreakfast("1", "1", "Deficit"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Deficit(With
                        .Any<DietFormModel>())));

        [Fact]
        public void AddDinnerShouldAddSuccessfullyAndRedirectToDeficitView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(FirstFood, FirstDiet))
            .Calling(c => c
                .AddDinner("1", "1", "Deficit"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Deficit(With
                        .Any<DietFormModel>())));

        [Fact]
        public void AddLunchShouldAddSuccessfullyAndRedirectToDeficitView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(FirstFood, FirstDiet))
            .Calling(c => c
                .AddLunch("1", "1", "Deficit"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Deficit(With
                        .Any<DietFormModel>())));
        
        [Fact]
        public void RemoveBreakfastShouldAddSuccessfullyAndRedirectToDeficitView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(breakfastFood))
            .Calling(c => c
                .RemoveBreakfast("1", "1", "Deficit"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Deficit(With
                        .Any<DietFormModel>())));
        
        [Fact]
        public void RemoveDinnerShouldAddSuccessfullyAndRedirectToDeficitView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(dinnerFood))
            .Calling(c => c
                .RemoveDinner("1", "1", "Deficit"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Deficit(With
                        .Any<DietFormModel>())));
        
        [Fact]
        public void RemoveLunchShouldAddSuccessfullyAndRedirectToDeficitView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(lunchFood))
            .Calling(c => c
                .RemoveLunch("1", "1", "Deficit"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Deficit(With
                        .Any<DietFormModel>())));

        //Balanced

        [Fact]
        public void BalancedShouldReturnCorrectView()
            => MyController<DietController>
            .Instance(instance => instance
                .WithData(GetUser)
                .WithUser("Testt"))
            .Calling(c => c
                .Balanced(new DietFormModel()))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void AddBreakfastShouldAddSuccessfullyAndRedirectToBalancedView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(FirstFood, FirstDiet))
            .Calling(c => c
                .AddBreakfast("1", "1", "Balanced"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Balanced(With
                        .Any<DietFormModel>())));

        [Fact]
        public void AddDinnerShouldAddSuccessfullyAndRedirectToBalancedView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(FirstFood, FirstDiet))
            .Calling(c => c
                .AddDinner("1", "1", "Balanced"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Balanced(With
                        .Any<DietFormModel>())));

        [Fact]
        public void AddLunchShouldAddSuccessfullyAndRedirectToBalancedView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(FirstFood, FirstDiet))
            .Calling(c => c
                .AddLunch("1", "1", "Balanced"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Balanced(With
                        .Any<DietFormModel>())));

        [Fact]
        public void RemoveBreakfastShouldAddSuccessfullyAndRedirectToBalancedView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(breakfastFood))
            .Calling(c => c
                .RemoveBreakfast("1", "1", "Balanced"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Balanced(With
                        .Any<DietFormModel>())));
        
        [Fact]
        public void RemoveDinnerShouldAddSuccessfullyAndRedirectToBalancedView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(dinnerFood))
            .Calling(c => c
                .RemoveDinner("1", "1", "Balanced"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Balanced(With
                        .Any<DietFormModel>())));
        
        [Fact]
        public void RemoveLunchShouldAddSuccessfullyAndRedirectToBalancedView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(lunchFood))
            .Calling(c => c
                .RemoveLunch("1", "1", "Balanced"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Balanced(With
                        .Any<DietFormModel>())));

        //Surplus

        [Fact]
        public void SurplusShouldReturnCorrectView()
            => MyController<DietController>
            .Instance(instance => instance
                .WithData(GetUser)
                .WithUser("Testt"))
            .Calling(c => c
                .Surplus(new DietFormModel()))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void AddBreakfastShouldAddSuccessfullyAndRedirectToSurplusView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(FirstFood, FirstDiet))
            .Calling(c => c
                .AddBreakfast("1", "1", "Surplus"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Surplus(With
                        .Any<DietFormModel>())));

        [Fact]
        public void AddDinnerShouldAddSuccessfullyAndRedirectToSurplusView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(FirstFood, FirstDiet))
            .Calling(c => c
                .AddDinner("1", "1", "Surplus"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Surplus(With
                        .Any<DietFormModel>())));

        [Fact]
        public void AddLunchShouldAddSuccessfullyAndRedirectToSurplusView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(FirstFood, FirstDiet))
            .Calling(c => c
                .AddLunch("1", "1", "Surplus"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Surplus(With
                        .Any<DietFormModel>())));

        [Fact]
        public void RemoveBreakfastShouldAddSuccessfullyAndRedirectToSurplusView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(breakfastFood))
            .Calling(c => c
                .RemoveBreakfast("1", "1", "Surplus"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Surplus(With
                        .Any<DietFormModel>())));
        
        [Fact]
        public void RemoveDinnerShouldAddSuccessfullyAndRedirectToSurplusView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(dinnerFood))
            .Calling(c => c
                .RemoveDinner("1", "1", "Surplus"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Surplus(With
                        .Any<DietFormModel>())));
        
        [Fact]
        public void RemoveLunchShouldAddSuccessfullyAndRedirectToSurplusView()
            => MyController<DietController>
            .Instance(i => i
                .WithData(lunchFood))
            .Calling(c => c
                .RemoveLunch("1", "1", "Surplus"))
            .ShouldHave()
            .ActionAttributes(a => a
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .Redirect(r => r
                .To<DietController>(d => d
                    .Surplus(With
                        .Any<DietFormModel>())));
    }
}
