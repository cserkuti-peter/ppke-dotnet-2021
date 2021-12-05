using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeBookApi.Dtos;
using RecipeBookApi.Filters;
using RecipeBookApi.Models;
using RecipeBookApi.Services;
using RecipeBookApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeBookService _recipeBookService;
        private readonly ILogger<RecipesController> _logger;

        public RecipesController(IRecipeBookService recipeBookService, ILogger<RecipesController> logger)
        {
            _recipeBookService = recipeBookService;
            _logger = logger;
        }

        // GET: api/<RecipesController>
        [HttpGet]
        public async Task<IEnumerable<RecipeRowVM>> GetAll([FromQuery]GenericQueryOption<RecipeFilter> option)
        {
            _logger.LogInformation("GetAll called.");
            //throw new InvalidOperationException("testing...");
            //throw new HttpResponseException("There was an error") { Status = 500 };
            return await _recipeBookService.GetAll(option);
        }

        // GET api/<RecipesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var r = await _recipeBookService.GetRecipeById(id);

            if (r == null)
                return NotFound();

            return Ok(r);
        }

        // POST api/recipebooks/{recipeBookId}/recipes/
        [HttpPost]
        public async Task<IActionResult> Post(int recipeBookId, [FromBody] NewRecipeDto r)
        {
            var createdRecipe = await _recipeBookService.CreateRecipe(recipeBookId, r);
            if (createdRecipe == null)
                return NotFound();

            return CreatedAtAction(nameof(Get), new { id = createdRecipe.Id }, createdRecipe);
        }

        // PUT api/<RecipesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateRecipeDto r)
        {
            return await _recipeBookService.UpdateRecipe(id, r)
                ? NoContent()
                : NotFound();
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = UserRoles.User)]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return await _recipeBookService.DeleteRecipe(id)
                ? NoContent()
                : NotFound();
        }
    }
}
