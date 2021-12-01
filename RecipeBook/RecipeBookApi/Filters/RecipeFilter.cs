using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.Filters
{
    public class RecipeFilter
    {
        public string NameTerm { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
    }
}
