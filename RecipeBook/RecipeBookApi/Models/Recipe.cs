using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Models
{
    [Table("Recipe")]
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Method { get; set; }
        [Range(1, 12000)]
        public int CookTimeMinutes { get; set; }
        [Range(1, 100)]
        public int Servers { get; set; }
        public double RatingsAvg { get; set; }

        public int RecipeBookId { get; set; }
        //[ForeignKey("RecipeBookId")]
        public RecipeBook RecipeBook { get; set; }

        public ICollection<CategoryRecipe> CategoryRecipes { get; set; }
    }
}
