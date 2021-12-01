using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.ViewModels
{
    public class RecipeRowVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double RatingsAvg { get; set; }
        public string RecipeBookName { get; set; }
    }
}
