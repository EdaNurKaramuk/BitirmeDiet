using DietCalculatorSystem.Data;
using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Models.Foods;
using DietCalculatorSystem.Services.Foods.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DietCalculatorSystem.Services.Foods
{
    public class FoodService : IFoodService
    {
        private readonly DietCalculatorDbContext data;

        public FoodService(DietCalculatorDbContext data)
        {
            this.data = data;
        }

        public void AcceptFood(string foodId)
        {
            var food = GetFood(foodId);

            food.IsPublic = true;

            this.data.SaveChanges();
        }

        public FoodQueryServiceModel All(int foodsPerPage = 5,
            int currentPage = 1,
            string searchTerm = null,
            FoodSorting foodSorting = FoodSorting.Calories)
        {
            var foodsAsQuery = data
                .Foods
                .Where(x => x.IsPublic == true);


            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                foodsAsQuery = foodsAsQuery
                    .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            foodsAsQuery = foodSorting switch
            {
                FoodSorting.Proteins => foodsAsQuery.OrderByDescending(x => x.Proteins),
                FoodSorting.Fats => foodsAsQuery.OrderByDescending(x => x.Fats),
                FoodSorting.Carbohydrates => foodsAsQuery.OrderByDescending(x => x.Carbohydrates),
                _ => foodsAsQuery.OrderByDescending(x => x.Calories),
            };

            var totalFoods = foodsAsQuery.Count();

            var allfoods = foodsAsQuery
                .Skip((currentPage - 1) * foodsPerPage)
                .Take(foodsPerPage)
                .Select(x => new FoodServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PictureUrl = x.PictureUrl,
                    Calories = x.Calories,
                    Proteins = x.Proteins,
                    Fats = x.Fats,
                    Carbohydrates = x.Carbohydrates,
                })
                .ToList();

            return new FoodQueryServiceModel
            {
                CurrentPage = currentPage,
                TotalFoods = totalFoods,
                FoodsPerPage = foodsPerPage,
                Foods = allfoods,
            };
        }

        public void CreateFood(string name,
            double? calories,
            double? proteins,
            double? fats,
            double? carbohydrates,
            string description,
            string pictureUrl)
        {
            Food food = new()
            {
                Name = name,
                Calories = calories,
                Proteins = proteins,
                Carbohydrates = carbohydrates,
                Fats = fats,
                Description = description,
                PictureUrl = pictureUrl,
                IsPublic = false
            };

            data.Foods.Add(food);

            data.SaveChanges();
        }

        public bool FoodExists(string foodName)
        {
            return this.data
                .Foods
                .Any(x => x.Name == foodName);
        }

        public List<Food> GetAllRequestedFoods()
        {
            return this.data
                .Foods
                .Where(x => x.IsPublic == false)
                .ToList();
        }

        public FoodDetailsServiceModel GetDetails(string foodId)
        {
            var allFoods = data
                .Foods
                .Where(x => x.IsPublic == true)
                .ToList();

            var mainFood = allFoods
                .FirstOrDefault(x => x.Id == foodId);

            allFoods.Remove(mainFood);

            Random rnd = new();

            var suggestedFoodOne = allFoods[rnd.Next(0, allFoods.Count)];

            allFoods.Remove(suggestedFoodOne);

            var suggestedFoodTwo = allFoods[rnd.Next(0, allFoods.Count)];

            return new FoodDetailsServiceModel
            {
                MainFood = mainFood,
                FirstSuggestedFood = suggestedFoodOne,
                SecondSuggestedFood = suggestedFoodTwo
            };
        }

        public Food GetFood(string id)
        {
            return data
                .Foods
                .FirstOrDefault(x => x.Id == id);
        }

        public Food GetRandomFood()
        {
            var allFoods = data
                .Foods
                .Where(x => x.IsPublic == true)
                .ToList();

            var count = allFoods.Count;

            Random rnd = new Random();

            return allFoods[rnd.Next(0, count)];
        }

        public void RemoveFood(string foodId)
        {
            var food = GetFood(foodId);

            data.Foods.Remove(food);
            data.SaveChanges();
        }
    }
}
