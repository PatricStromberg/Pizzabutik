using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiPizza.Models
{
    public class EditViewModel
    {
        public Category Category { get; set; }
        public Ingredient Ingredient { get; set; }
        public Dish Dish { get; set; }
        public List<Ingredient> AllIngredients { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
    }
}
