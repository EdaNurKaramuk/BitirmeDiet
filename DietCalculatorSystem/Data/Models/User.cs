using DietCalculatorSystem.Data.Models.OneToOneRelationships;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Data.Models
{
    using static DataConstants.User;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(MaxFullNameLength)]
        public string FullName { get; set; }

        public string BalancedDietId { get; set; }
        public BalancedDiet BalancedDiet { get; set; }

        public string DeficitDietId { get; set; }
        public DeficitDiet DeficitDiet { get; set; }

        public string SurplusDietId { get; set; }
        public SurplusDiet SurplusDiet { get; set; }
    }
}
