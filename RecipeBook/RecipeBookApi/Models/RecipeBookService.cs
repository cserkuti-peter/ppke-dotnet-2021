using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Models
{
    public interface IRecipeBookService
    { }

    public class RecipeBookService : IRecipeBookService
    {
        private List<Recipe> recipes = new List<Recipe>
        { 
            new Recipe
            { 
                Id = 1,
                Name = "Apple pie",
                Ingredients = "3 eggs, 2 tbsp milk, ...",
                Method = "1. Separate eggs 2...",
                CookTimeMinutes = 60,
                Servers = 12
            }
        };

        public Recipe CreateRecipe(Recipe r)
        {
            r.Id = recipes.Max(x => x.Id) + 1;
            recipes.Add(r);
            return r;
        }

        public List<Recipe> GetAll()
        {
            return recipes.ToList();
        }

        public List<Recipe> GetRecipesWhere(Func<Recipe, bool> predicate)
        {
            return recipes.Where(predicate).ToList();
        }

        public Recipe GetRecipeById(int id)
        {
            return recipes.SingleOrDefault(x => x.Id == id);
        }

        public bool UpdateRecipe(int id, Recipe r)
        {
            var recipeToUpdate = recipes.SingleOrDefault(x => x.Id == id);

            if (recipeToUpdate == null)
                return false;

            recipeToUpdate.Name = r.Name;
            recipeToUpdate.Method = r.Method;
            recipeToUpdate.Ingredients = r.Ingredients;

            return true;
        }

        public bool DeleteRecipe(int id)
        {
            var n = recipes.RemoveAll(x => x.Id == id);

            return n == 1;
        }
    }
}
