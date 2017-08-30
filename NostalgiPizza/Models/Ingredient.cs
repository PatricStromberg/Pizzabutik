﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiPizza.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public bool Enable { get; set; }
    }
}
