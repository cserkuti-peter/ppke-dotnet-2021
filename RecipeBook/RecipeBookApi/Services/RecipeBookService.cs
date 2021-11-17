using Microsoft.EntityFrameworkCore;
using RecipeBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipeBookApi.Services
{
    public class RecipeBookService : IRecipeBookService
    {
        private readonly RecipeBookContext _context;

        public RecipeBookService(RecipeBookContext recipeBookContext)
        {
            this._context = recipeBookContext;
        }

        public async Task<Recipe> CreateRecipe(Recipe r)
        {
            _context.Recipes.Add(r);
            await _context.SaveChangesAsync();

            return r;
        }

        public async Task<bool> DeleteRecipe(int id)
        {
            var r = await _context.Recipes.FindAsync(id);
            if (r == null)
                return false;

            _context.Recipes.Remove(r);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Recipe>> GetAll()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<Recipe> GetRecipeById(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async Task<List<Recipe>> GetRecipesWhere(Expression<Func<Recipe, bool>> predicate)
        {
            return await _context.Recipes.Where(predicate).ToListAsync();
        }

        public async Task<bool> UpdateRecipe(int id, Recipe r)
        {
            if (id != r.Id)
                throw new ArgumentException();

            _context.Entry(r).State = EntityState.Modified;
            var n = await _context.SaveChangesAsync();

            return n == 1;
        }
    }
}
