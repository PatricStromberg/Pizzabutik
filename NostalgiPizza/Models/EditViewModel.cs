using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NostalgiPizza.Models
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public List<Ingredient> AllIngredients { get; set; }
        public int CategoryId { get; set; }
        [DisplayName("Category")]
        public SelectList CategoryList { get; set; }
    }
}
