using DietCalculatorSystem.Data;
using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Data.Models.OneToOneRelationships;
using System.Linq;

namespace DietCalculatorSystem.Services.Users
{
    public class UserService : IUserService
    {
        private readonly DietCalculatorDbContext data;

        public UserService(DietCalculatorDbContext data)
        {
            this.data = data;
        }
        public User CreateUser(string fullname, string email)
        {
            var user = new User
            {
                FullName = fullname,
                UserName = fullname,
                Email = email
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

            this.data.Diets.AddRange(balanced, deficit, surplus);

            this.data.SaveChanges();

            return user;
        }

        public void DeleteDiets(string balancedDietId, string deficitDietId, string surplusDietId)
        {
            this.data.BalancedDiets.Remove(this.data.BalancedDiets.FirstOrDefault(x => x.DietId == balancedDietId));
            this.data.DeficitDiets.Remove(this.data.DeficitDiets.FirstOrDefault(x => x.DietId == deficitDietId));
            this.data.SurplusDiets.Remove(this.data.SurplusDiets.FirstOrDefault(x => x.DietId == surplusDietId));

            this.data.SaveChanges();
        }
    }
}
