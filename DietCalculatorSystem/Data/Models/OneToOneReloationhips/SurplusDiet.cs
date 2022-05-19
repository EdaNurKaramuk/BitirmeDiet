using System.ComponentModel.DataAnnotations;

namespace DietCalculatorSystem.Data.Models.OneToOneRelationships
{
    public class SurplusDiet
    {
        [Key]
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Key]
        [Required]
        public string DietId { get; set; }
        public Diet Diet { get; set; }
    }
}
