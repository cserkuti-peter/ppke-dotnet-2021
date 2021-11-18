using RecipeBookApi.Dtos;
using RecipeBookApi.Models;
using RecipeBookApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipeBookApi.Services
{
    public interface IRecipeBookService
    {
        Task<RecipeVM> CreateRecipe(NewRecipeDto r);
        Task<bool> DeleteRecipe(int id);
        Task<List<RecipeRowVM>> GetAll();
        Task<RecipeVM> GetRecipeById(int id);
        Task<List<RecipeRowVM>> GetRecipesWhere(Expression<Func<Recipe, bool>> predicate);
        Task<bool> UpdateRecipe(int id, UpdateRecipeDto r);
    }
}