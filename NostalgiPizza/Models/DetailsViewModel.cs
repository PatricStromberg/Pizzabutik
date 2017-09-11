using System.Collections.Generic;

namespace NostalgiPizza.Models
{
    public class DetailsViewModel
    {
        public List<Dish> Dishes { get; set; }
        public List<Category> Categories { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Order> Orders { get; set; }
    }
}
