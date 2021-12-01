using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RecipeBookApi.Dtos;
using RecipeBookApi.Filters;
using RecipeBookApi.Models;
using RecipeBookApi.ViewModels;
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
        private readonly IMapper _mapper;

        public RecipeBookService(RecipeBookContext recipeBookContext, IMapper mapper)
        {
            this._context = recipeBookContext;
            _mapper = mapper;
        }

        public async Task<RecipeVM> CreateRecipe(int recipeBookId, NewRecipeDto r)
        {
            var recipe = _mapper.Map<Recipe>(r);
            recipe.RatingsAvg = 0;

            var recipeBook = await _context.RecipeBooks.FindAsync(recipeBookId);
            if (recipeBook == null)
                return null;

            recipeBook.RecipesNumber++;

            //  1. Set foreign key by id
            recipe.RecipeBookId = recipeBookId;

            //  2. Set foreign key by navigation property
            //recipe.RecipeBook = recipeBook;

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeVM>(recipe);
        }

        public async Task<bool> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return false;

            //  1. Get the booko by a separate query
            var recipeBook = await _context.RecipeBooks.FindAsync(recipe.RecipeBookId);

            //  2. Explicit loading
            //await _context
            //    .Entry(recipe)
            //    .Reference(x => x.RecipeBook)
            //    .LoadAsync();

            //  3. Lazy loading (Microsoft.EntityFrameworkCore.Proxies package)
            //recipe.RecipeBook

            recipeBook.RecipesNumber--;

            _context.Recipes.Remove(recipe);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<RecipeRowVM>> GetAll(GenericQueryOption<RecipeFilter> option)
        {
            var q = _context.Recipes as IQueryable<Recipe>;
            if (!String.IsNullOrEmpty(option.Filter?.NameTerm))
            {
                q = q.Where(x => x.Name.Contains(option.Filter.NameTerm));
            }
            if (option.Filter?.MinRating != null)
            {
                q = q.Where(x => x.RatingsAvg >= option.Filter.MinRating);
            }
            if (option.Filter?.MaxRating != null)
            {
                q = q.Where(x => x.RatingsAvg <= option.Filter.MaxRating);
            }

            q = option.SortOrder == SortOrder.Ascending
                ? q.OrderBy(x => x.Name)
                : q.OrderByDescending(x => x.Name);

            return await q
                .Skip((option.Page - 1) * option.PageSize)
                .Take(option.PageSize)
                .ProjectTo<RecipeRowVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<RecipeVM> GetRecipeById(int id)
        {
            return await _context.Recipes
                .Where(x => x.Id == id)
                //.Select(x => new RecipeVM
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    RatingsAvg = x.RatingsAvg,
                //    CookTimeMinutes = x.CookTimeMinutes,
                //    Ingredients = x.Ingredients,
                //    Method = x.Method,
                //    Servers = x.Servers
                //})
                //.Select(x => _mapper.Map<RecipeVM>(x))
                .ProjectTo<RecipeVM>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<List<RecipeRowVM>> GetRecipesWhere(Expression<Func<Recipe, bool>> predicate)
        {
            return await _context.Recipes
                .Where(predicate)
                //.Select(x => new RecipeRowVM
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    RatingsAvg = x.RatingsAvg
                //})
                //.Select(x => _mapper.Map<RecipeRowVM>(x))
                .ProjectTo<RecipeRowVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<bool> UpdateRecipe(int id, UpdateRecipeDto r)
        {
            //var m = new Recipe
            //{
            //    Id = id,
            //    CookTimeMinutes = r.CookTimeMinutes,
            //    Ingredients = r.Ingredients,
            //    Method = r.Method,
            //    Name = r.Name,
            //    Servers = r.Servers
            //};
            var m = _mapper.Map<Recipe>(r);
            m.Id = id;

            _context.Entry(r).State = EntityState.Modified;
            var n = await _context.SaveChangesAsync();

            return n == 1;
        }
    }
}
