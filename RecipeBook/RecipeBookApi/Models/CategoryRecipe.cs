using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Models
{
    [Table("CategoryRecipe")]
    public class CategoryRecipe
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
