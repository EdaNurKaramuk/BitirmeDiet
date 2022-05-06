namespace DietCalculatorSystem.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int MinFullNameLength = 5;
            public const int MaxFullNameLength = 40;
            public const int MinPasswordLength = 6;
            public const int MaxPasswordLength = 100;
        }

        public class Food
        {
            public const int MinFoodNameLength = 2;
            public const int MaxFoodNameLength = 40;
            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 200;
            public const int MinCalories = 0;
            public const int MaxCalories = 900;
            public const int MinProteins = 0;
            public const int MaxProteins = 100;
            public const int MinFats = 0;
            public const int MaxFats = 100;
            public const int MinCarbohydrates = 0;
            public const int MaxCarbohydrates = 100;
            public const string ValidNumberRegex = @"^\d+[\.\d]*$";
        }

        public class Calculator
        {
            public const int MinAge = 15;
            public const int MaxAge = 90;
            public const double MinWeight = 20;
            public const double MaxWeight = 500;
            public const int MinHeight = 40;
            public const int MaxHeight = 300;
        }
    }
}
