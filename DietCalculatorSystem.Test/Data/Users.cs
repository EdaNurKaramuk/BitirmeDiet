using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Data.Models.OneToOneRelationships;

namespace DietCalculatorSystem.Test.Data
{
    public static class Users
    {
        public static User GetUser
            => CreateUser();

        private static User CreateUser() 
        {
            var user = new User
            {
                Id="1",
                FullName = "Testt",
                UserName = "Testt",
                Email = "Test@mail.bg"
            };
            var balanced = new Diet()
            {
                TotalCalories = 0,
                TotalProteins = 0,
                TotalFats = 0,
                TotalCarbohydrates = 0,
            };
            var deficit = new Diet()
            {
                TotalCalories = 0,
                TotalProteins = 0,
                TotalFats = 0,
                TotalCarbohydrates = 0,
            };
            var surplus = new Diet()
            {
                TotalCalories = 0,
                TotalProteins = 0,
                TotalFats = 0,
                TotalCarbohydrates = 0,
            };

            var balancedDiet = new BalancedDiet
            {
                User = user,
                UserId = user.Id,
                Diet = balanced,
                DietId = balanced.Id
            };

            var deficitDiet = new DeficitDiet
            {
                User = user,
                UserId = user.Id,
                Diet = deficit,
                DietId = deficit.Id
            };

            var surplusDiet = new SurplusDiet
            {
                User = user,
                UserId = user.Id,
                Diet = surplus,
                DietId = surplus.Id
            };

            user.BalancedDiet = balancedDiet;
            user.BalancedDietId = balanced.Id;
            user.DeficitDiet = deficitDiet;
            user.DeficitDietId = deficit.Id;
            user.SurplusDiet = surplusDiet;
            user.SurplusDietId = surplus.Id;

            return user;
        }
    }
}
