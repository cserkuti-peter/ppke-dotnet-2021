using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBookApi.ViewModels.Authentication
{
    public class LoginViewModel
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
