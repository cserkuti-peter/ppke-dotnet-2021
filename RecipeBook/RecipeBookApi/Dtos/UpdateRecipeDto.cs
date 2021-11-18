using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Dtos
{
    public class UpdateRecipeDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Method { get; set; }
        [Range(1, 12000)]
        public int CookTimeMinutes { get; set; }
        [Range(1, 100)]
        public int Servers { get; set; }
    }
}
