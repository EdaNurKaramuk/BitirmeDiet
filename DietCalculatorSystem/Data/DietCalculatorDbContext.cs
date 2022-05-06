using DietCalculatorSystem.Data.Models;
using DietCalculatorSystem.Data.Models.ManyToManyRelationships;
using DietCalculatorSystem.Data.Models.OneToOneRelationships;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DietCalculatorSystem.Data
{
    public class DietCalculatorDbContext : IdentityDbContext<User>
    {
        public DietCalculatorDbContext(DbContextOptions<DietCalculatorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Food> Foods { get; init; }
        //public DbSet<TotalFood> TotalFoods { get; init; }
        public DbSet<BreakfastFood> BreakfastFoods { get; init; }
        public DbSet<LunchFood> LunchFoods { get; init; }
        public DbSet<DinnerFood> DinnerFoods { get; init; }
        public DbSet<BalancedDiet> BalancedDiets { get; init; }
        public DbSet<DeficitDiet> DeficitDiets { get; init; }
        public DbSet<SurplusDiet> SurplusDiets { get; init; }
        public DbSet<Diet> Diets { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BalancedDiet>()
                .HasKey(x => new { x.UserId, x.DietId });

            builder.Entity<DeficitDiet>()
                .HasKey(x => new { x.UserId, x.DietId });

            builder.Entity<SurplusDiet>()
            .HasKey(x => new { x.UserId, x.DietId });

            builder.Entity<TotalFood>()
                .HasKey(x => new { x.DietId, x.FoodId });

            builder.Entity<BreakfastFood>()
                .HasKey(x => new { x.DietId, x.FoodId });

            builder.Entity<LunchFood>()
                .HasKey(x => new { x.DietId, x.FoodId });

            builder.Entity<DinnerFood>()
                .HasKey(x => new { x.DietId, x.FoodId });

            builder.Entity<User>()
                .HasOne(x => x.BalancedDiet)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasOne(x => x.DeficitDiet)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasOne(x => x.SurplusDiet)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
