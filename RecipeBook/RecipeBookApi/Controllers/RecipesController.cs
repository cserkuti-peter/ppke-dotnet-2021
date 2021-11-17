using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeBookApi.Models;
using RecipeBookApi.Services;
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
        public async Task<IEnumerable<Recipe>> GetAll()
        {
            _logger.LogInformation("GetAll called.");
            //throw new InvalidOperationException("testing...");
            //throw new HttpResponseException("There was an error") { Status = 500 };
            return await _recipeBookService.GetAll();
        }

        // GET api/<RecipesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var r = _recipeBookService.GetRecipeById(id);

            if (r == null)
                return NotFound();

            return Ok(r);
        }

        // POST api/<RecipesController>
        [HttpPost]
        public IActionResult Post([FromBody] Recipe r)
        {
            var createdRecipe = _recipeBookService.CreateRecipe(r);

            return CreatedAtAction(nameof(Get), new { id = createdRecipe.Id }, createdRecipe);
        }

        // PUT api/<RecipesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Recipe r)
        {
            return await _recipeBookService.UpdateRecipe(id, r)
                ? NoContent()
                : NotFound();
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _recipeBookService.DeleteRecipe(id)
                ? NoContent()
                : NotFound();
        }
    }
}
