﻿using System.Collections.Generic;

namespace NostalgiPizza.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
