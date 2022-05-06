using DietCalculatorSystem.Data.Models;

namespace DietCalculatorSystem.Services.Users
{
    public interface IUserService
    {
        User CreateUser(string fullname,
            string email);

        void DeleteDiets(string balancedDietId,
            string deficitDietId,
            string surplusDietId);
    }
}
