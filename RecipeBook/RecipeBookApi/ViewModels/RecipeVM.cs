using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.ViewModels
{
    public class RecipeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Method { get; set; }
        public int CookTime { get; set; }
        public int Servers { get; set; }
        public double Rating { get; set; }
    }
}
