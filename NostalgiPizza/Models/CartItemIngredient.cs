﻿namespace NostalgiPizza.Models
{
    public class CartItemIngredient
    {
        public int CartItemId { get; set; }
        public CartItem CartItem { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int IngredientPrice { get; set; }
        public bool Enabled { get; set; }
    }
}
