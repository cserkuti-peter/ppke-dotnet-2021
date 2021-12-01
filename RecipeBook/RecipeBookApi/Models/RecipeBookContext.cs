using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Models
{
    public class RecipeBookContext : DbContext
    {
        public RecipeBookContext(DbContextOptions<RecipeBookContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Fluent API
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.RecipeBook)
                .WithMany(rb => rb.Recipes)
                .HasForeignKey(r => r.RecipeBookId);

            modelBuilder.Entity<CategoryRecipe>().HasKey(cr => new { cr.CategoryId, cr.RecipeId });

            //  Seed/populate database
            modelBuilder.Entity<RecipeBook>().HasData(
                new RecipeBook { Id = 1, Name = "My favorite recipes", Description = "...", RecipesNumber = 2 });
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { Id = 1, RecipeBookId = 1, Name = "Apple pie", Ingredients = "Eggs, ...", Method = "...", CookTimeMinutes = 120, Servers = 10, RatingsAvg = 0 },
                new Recipe { Id = 2, RecipeBookId = 1, Name = "Sushi", Ingredients = "Rice, ...", Method = "...", CookTimeMinutes = 20, Servers = 10, RatingsAvg = 0 }
            );
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeBook> RecipeBooks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
