using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NostalgiPizza.Models
{
    public class CartItemIngredient
    {
        public CartItem CartItem { get; set; }
        public int CartItemId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int IngredientId { get; set; }
        public bool Enabled { get; set; }
    }
}
