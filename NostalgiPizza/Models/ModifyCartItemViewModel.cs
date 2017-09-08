using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiPizza.Models
{
    public class ModifyCartItemViewModel
    {
        public List<Ingredient> AllIngredients { get; set; }
        public CartItem CartItem { get; set; }
    }
}
