using DietCalculatorSystem.Data;
using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Data.Models.ManyToManyRelationships;
using DietCalculatorSystem.Services.Diets.Models;
using DietCalculatorSystem.Services.Foods;
using DietCalculatorSystem.Services.Foods.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DietCalculatorSystem.Services.Diets
{
    public class DietService : IDietService
    {
        private readonly DietCalculatorDbContext data;
        private readonly IFoodService foods;

        public DietService(DietCalculatorDbContext data,
            IFoodService foods)
        {
            this.data = data;
            this.foods = foods;
        }

        public void AddBreakfastFood(string foodId,
            string dietId)
        {
            var diet = data
                .Diets
                .FirstOrDefault(a => a.Id == dietId);

            var food = foods.GetFood(foodId);

            var currBreakfastFood = data
                .BreakfastFoods
                .FirstOrDefault(x => x.FoodId == food.Id && x.DietId == dietId);

            if (currBreakfastFood == null)
            {
                currBreakfastFood = new BreakfastFood
                {
                    Diet = diet,
                    DietId = dietId,
                    Food = food,
                    FoodId = foodId
                };

                diet.BreakfastFoods.Add(currBreakfastFood);
                data.BreakfastFoods.Add(currBreakfastFood);
            }

            currBreakfastFood.Quantity++;

            data.SaveChanges();
        }

        public void AddDinnerFood(string foodId,
            string dietId)
        {
            var diet = data
                .Diets
                .FirstOrDefault(a => a.Id == dietId);

            var food = data
                .Foods
                .FirstOrDefault(a => a.Id == foodId);

            var Food = data
                .DinnerFoods
                .FirstOrDefault(x => x.FoodId == food.Id && x.DietId == dietId);

            if (Food == null)
            {
                Food = new DinnerFood
                {
                    Diet = diet,
                    DietId = dietId,
                    Food = food,
                    FoodId = foodId
                };

                diet.DinnerFoods.Add(Food);
                data.DinnerFoods.Add(Food);
            }

            Food.Quantity++;

            data.SaveChanges();
        }

        public void AddLunchFood(string foodId,
            string dietId)
        {
            var diet = data
                .Diets  
                .FirstOrDefault(a => a.Id == dietId);

            var food = data
                .Foods
                .FirstOrDefault(a => a.Id == foodId);

            var Food = data
                .LunchFoods
                .FirstOrDefault(x => x.FoodId == food.Id && x.DietId == dietId);

            if (Food == null)
            {
                Food = new LunchFood
                {
                    Diet = diet,
                    DietId = dietId,
                    Food = food,
                    FoodId = foodId
                };

                diet.LunchFoods.Add(Food);
                data.LunchFoods.Add(Food);
            }

            Food.Quantity++;

            data.SaveChanges();
        }

        public DietServiceModel GetAllMeals(string dietId)
        {
            var diet = data
                .Diets
                .Include(d => d.BreakfastFoods)
                .ThenInclude(bf => bf.Food)
                .Include(d => d.LunchFoods)
                .ThenInclude(lf => lf.Food)
                .Include(d => d.DinnerFoods)
                .ThenInclude(df => df.Food)
                .FirstOrDefault(d => d.Id == dietId);

            //Breakfast
            var breakfastFoods = diet
                .BreakfastFoods
                .Select(a => new FoodServiceModel
                {
                    Id = a.Food.Id,
                    Name = a.Food.Name,
                    Calories = a.Food.Calories,
                    Proteins = a.Food.Proteins,
                    Fats = a.Food.Fats,
                    Carbohydrates = a.Food.Carbohydrates,
                    Quantity = a.Quantity
                })
                .ToList();

            //Lunch
            var lunchFoods = diet
                .LunchFoods
                .Select(a => new FoodServiceModel
                {
                    Id = a.Food.Id,
                    Name = a.Food.Name,
                    Calories = a.Food.Calories,
                    Proteins = a.Food.Proteins,
                    Fats = a.Food.Fats,
                    Carbohydrates = a.Food.Carbohydrates,
                    Quantity = a.Quantity
                })
                .ToList();

            //Dinner
            var dinnerFoods = diet
                .DinnerFoods
                .Select(a => new FoodServiceModel
                {
                    Id = a.Food.Id,
                    Name = a.Food.Name,
                    Calories = a.Food.Calories,
                    Proteins = a.Food.Proteins,
                    Fats = a.Food.Fats,
                    Carbohydrates = a.Food.Carbohydrates,
                    Quantity = a.Quantity
                })
                .ToList();

            return new DietServiceModel
            {
                TotalCalories = diet.TotalCalories,
                TotalProteins = diet.TotalProteins,
                TotalFats = diet.TotalFats,
                TotalCarbohydrates = diet.TotalCarbohydrates,
                BreakfastFoods = breakfastFoods,
                DinnerFoods = dinnerFoods,
                LunchFoods = lunchFoods,
            };
        }

        public void CalculateDiet(string name,
            int? age,
            string gender,
            double? height,
            double? weight,
            double? activity)
        {
            var user = data.Users.FirstOrDefault(x => x.FullName == name);

            var balancedDiet = data
                .BalancedDiets
                .Include(a => a.Diet)
                .FirstOrDefault(x => x.User == user)
                .Diet;

            var surplusDiet = data
                .SurplusDiets
                .Include(a => a.Diet)
                .FirstOrDefault(x => x.User == user)
                .Diet;

            var deficitDiet = data
                .DeficitDiets
                .Include(a => a.Diet)
                .FirstOrDefault(x => x.User == user)
                .Diet;

            var calories = gender == "Male" ? CalculateMaleCalories(age, height, weight, activity) :
                                                             CalculateFemaleCalories(age, height, weight, activity);

            var percent = (double)(calories * 0.15);

            balancedDiet.TotalCalories = Math.Round((double)calories, 2);
            surplusDiet.TotalCalories = Math.Round((double)calories + percent, 2);
            deficitDiet.TotalCalories = Math.Round((double)calories - percent, 2);

            CalculateMacros(balancedDiet, weight);
            CalculateMacros(surplusDiet, weight);
            CalculateMacros(deficitDiet, weight);

            SplitMeals(balancedDiet);
            SplitMeals(surplusDiet);
            SplitMeals(deficitDiet);

            data.SaveChanges();
        }

        public string GetDietId(string name,
            string methodName)
        {
            if (methodName == "Balanced")
            {
                return this.data
                    .BalancedDiets
                    .FirstOrDefault(x => x.User.FullName == name)
                    .DietId;
            }
            else if (methodName == "Deficit")
            {
                return this.data
                   .DeficitDiets
                   .FirstOrDefault(x => x.User.FullName == name)
                   .DietId;
            }
            else
            {
                return this.data
                   .SurplusDiets
                   .FirstOrDefault(x => x.User.FullName == name)
                   .DietId;
            }
        }

        public void RemoveBreakFastFood(string foodId,
            string dietId)
        {
            var breakfastFood = data
                            .BreakfastFoods
                            .FirstOrDefault(x => x.FoodId == foodId && x.DietId == dietId);

            breakfastFood.Quantity--;

            if (breakfastFood.Quantity == 0)
            {
                data.BreakfastFoods.Remove(breakfastFood);
            }

            data.SaveChanges();
        }

        public void RemoveDinnerFood(string foodId,
            string dietId)
        {
            var dinnerFood = data
                            .DinnerFoods
                            .FirstOrDefault(x => x.FoodId == foodId && x.DietId == dietId);

            dinnerFood.Quantity--;

            if (dinnerFood.Quantity == 0)
            {
                data.DinnerFoods.Remove(dinnerFood);
            }

            data.SaveChanges();
        }

        public void RemoveLunchFood(string foodId,
            string dietId)
        {
            var lunchFood = data
                            .LunchFoods
                            .FirstOrDefault(x => x.FoodId == foodId && x.DietId == dietId);

            lunchFood.Quantity--;

            if (lunchFood.Quantity == 0)
            {
                data.LunchFoods.Remove(lunchFood);
            }

            data.SaveChanges();
        }

        private static void SplitMeals(Diet diet)
        {
            var mealCalories = Math.Round((double)diet.TotalCalories / 3, 2);
            var mealProteins = Math.Round((double)diet.TotalProteins / 3, 2);
            var mealFats = Math.Round((double)diet.TotalFats / 3, 2);
            var mealCarbohydrates = Math.Round((double)diet.TotalCarbohydrates / 3, 2);

            //Breakfast
            diet.BreakfastCalories = mealCalories;
            diet.BreakfastProteins = mealProteins;
            diet.BreakfastFats = mealFats;
            diet.BreakfastCarbohydrates = mealCarbohydrates;

            //Lunch
            diet.LunchCalories = mealCalories;
            diet.LunchProteins = mealProteins;
            diet.LunchFats = mealFats;
            diet.LunchCarbohydrates = mealCarbohydrates;

            //Dinner
            diet.DinnerCalories = mealCalories;
            diet.DinnerProteins = mealProteins;
            diet.DinnerFats = mealFats;
            diet.DinnerCarbohydrates = mealCarbohydrates;

        }

        private static void CalculateMacros(Diet diet,
            double? weight)
        {
            var caloriesLeft = diet.TotalCalories;

            diet.TotalProteins = Math.Round((double)weight * 1.8, 2);


            diet.TotalFats = Math.Round((double)(caloriesLeft * 0.20) / 9, 2);

            caloriesLeft -= diet.TotalProteins * 4;

            caloriesLeft -= diet.TotalFats * 9;

            diet.TotalCarbohydrates = Math.Round((double)caloriesLeft / 4, 2);
        }

        private static double? CalculateFemaleCalories(int? age,
            double? height,
            double? weight,
            double? activity)
        {
            double? MSJEquation = ((10 * weight) +
                                              (6.25 * height) -
                                              (5 * age) - 161) *
                                              activity;

            double? RHBEquation = ((9.247 * weight) +
                                  (3.098 * height) -
                                  (4.330 * age) + 447.593) *
                                  activity;

            return (MSJEquation + RHBEquation) / 2;
        }

        private static double? CalculateMaleCalories(int? age,
            double? height,
            double? weight,
            double? activity)
        {
            double? MSJEquation = ((10 * weight) +
                                  (6.25 * height) -
                                  (5 * age) + 5) *
                                  activity;

            double? RHBEquation = ((13.397 * weight) +
                                  (4.799 * height) -
                                  (5.677 * age) + 88.362) *
                                  activity;

            return (MSJEquation + RHBEquation) / 2;
        }
    }
}
