using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CategoryRecipe> CategoryRecipes { get; set; }
    }
}
