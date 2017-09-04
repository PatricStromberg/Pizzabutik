using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NostalgiPizza.Models
{
    public class CreateViewModel
    {
        public string NewName { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public SelectList CategoryList { get; set; }
        public List<Ingredient> IngredientList { get; set; }
    }
}
