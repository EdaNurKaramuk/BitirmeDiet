using System.ComponentModel.DataAnnotations;

using static DietCalculatorSystem.Data.DataConstants.Food;

namespace DietCalculatorSystem.Models.Foods
{

    public class AddFoodFormModel
    {
        [Required(ErrorMessage = "Yemek İsmi alanı boş bırakılamaz.")]
        [StringLength(MaxFoodNameLength, ErrorMessage = "{0} en az {2} ve en fazla {1} karakter uzunluğunda olmalı.", MinimumLength = MinFoodNameLength)]
        [Display(Name = "Yemek İsmi")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Açıklama alanı boş bırakılamaz.")]
        [StringLength(MaxDescriptionLength, ErrorMessage = "{0} en az {2} ve en fazla {1} karakter uzunluğunda olmalı.", MinimumLength = MinDescriptionLength)]
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Resim URL alanı boş bırakılamaz.")]
        [Url(ErrorMessage = "Geçerli bir URL girin!")]
        [Display(Name = "Resim URL")]
        public string PictureUrl { get; set; }

        [Required(ErrorMessage = "Kalori alanı boş bırakılamaz.")]
        [RegularExpression(ValidNumberRegex, ErrorMessage = "Geçersiz değer! Girilen değer ',' yerine '.' sembolü ile ayrılmalı!")]
        [Range(MinCalories, MaxCalories, ErrorMessage = "{1} ile {2} aralığında bir değer girin.")]
        public double? Calories { get; set; }

        [Required(ErrorMessage = "Protein alanı boş bırakılamaz.")]
        [RegularExpression(ValidNumberRegex, ErrorMessage = "Geçersiz değer! Girilen değer ',' yerine '.' sembolü ile ayrılmalı!")]
        [Range(MinProteins, MaxProteins, ErrorMessage = "{1} ile {2} aralığında bir değer girin.")]
        public double? Proteins { get; set; }

        [Required(ErrorMessage = "Yağ alanı boş bırakılamaz.")]
        [RegularExpression(ValidNumberRegex, ErrorMessage = "Geçersiz değer! Girilen değer ',' yerine '.' sembolü ile ayrılmalı!")]
        [Range(MinFats, MaxFats, ErrorMessage = "{1} ile {2} aralığında bir değer girin.")]
        public double? Fats { get; set; }

        [Required(ErrorMessage = "Karbonhidrat alanı boş bırakılamaz.")]
        [RegularExpression(ValidNumberRegex, ErrorMessage = "Geçersiz değer! Girilen değer ',' yerine '.' sembolü ile ayrılmalı!")]
        [Range(MinCarbohydrates, MaxCarbohydrates, ErrorMessage = "{1} ile {2} aralığında bir değer girin.")]
        public double? Carbohydrates { get; set; }
    }
}
