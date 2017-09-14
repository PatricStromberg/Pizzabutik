using System.Collections.Generic;

namespace NostalgiPizza.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public List<CartItemIngredient> CartItemIngredients { get; set; }
        public int Price { get; set; }
        public bool Enable { get; set; }
    }
}
