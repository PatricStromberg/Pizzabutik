using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiPizza.Models
{
    public class AddToCartViewModel
    {
        public List<Ingredient> AllIngredients { get; set; }
        public Dish Dish { get; set; }
    }
}
