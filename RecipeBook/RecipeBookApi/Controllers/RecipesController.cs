using Microsoft.AspNetCore.Mvc;
using RecipeBookApi.Models;
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
        private readonly RecipeBookService _recipeBookService;

        public RecipesController(RecipeBookService recipeBookService)
        {
            _recipeBookService = recipeBookService;
        }

        // GET: api/<RecipesController>
        [HttpGet]
        public IEnumerable<Recipe> GetAll()
        {
            return _recipeBookService.GetAll();
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
        public IActionResult Put(int id, [FromBody] Recipe r)
        {
            return _recipeBookService.UpdateRecipe(id, r)
                ? NoContent()
                : NotFound();
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _recipeBookService.DeleteRecipe(id)
                ? NoContent()
                : NotFound();
        }
    }
}
