using RecipeBookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipeBookApi.Services
{
    public interface IRecipeBookService
    {
        Task<Recipe> CreateRecipe(Recipe r);
        Task<bool> DeleteRecipe(int id);
        Task<List<Recipe>> GetAll();
        Task<Recipe> GetRecipeById(int id);
        Task<List<Recipe>> GetRecipesWhere(Expression<Func<Recipe, bool>> predicate);
        Task<bool> UpdateRecipe(int id, Recipe r);
    }
}