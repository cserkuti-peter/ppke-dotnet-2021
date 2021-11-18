using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RecipeBookApi.Dtos;
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

        public async Task<RecipeVM> CreateRecipe(NewRecipeDto r)
        {
            //var m = new Recipe
            //{
            //    CookTimeMinutes = r.CookTimeMinutes,
            //    Ingredients = r.Ingredients,
            //    Method = r.Method,
            //    Name = r.Name,
            //    RatingsAvg = 0,
            //    Servers = r.Servers
            //};
            var m = _mapper.Map<Recipe>(r);
            m.RatingsAvg = 0;

            _context.Recipes.Add(m);
            await _context.SaveChangesAsync();

            //return new RecipeVM
            //{ 
            //    Id = m.Id,
            //    Servers = m.Servers,
            //    RatingsAvg = m.RatingsAvg,
            //    Name = m.Name,
            //    Method = m.Method,
            //    Ingredients = m.Ingredients,
            //    CookTimeMinutes = m.CookTimeMinutes
            //};
            return _mapper.Map<RecipeVM>(m);
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

        public async Task<List<RecipeRowVM>> GetAll()
        {
            return await _context.Recipes
                //  1. Manual
                //.Select(x => new RecipeRowVM
                //{ 
                //    Id = x.Id,
                //    Name = x.Name,
                //    RatingsAvg = x.RatingsAvg
                //})
                //  2. Automapper: in memory
                //.Select(x => _mapper.Map<RecipeRowVM>(x))
                //  3. Automapper: building the expression tree
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
