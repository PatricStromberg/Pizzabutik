using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NostalgiPizza.Models
{
    public class CreateViewModel
    {
        [DisplayName("Name")]
        public string NewName { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        [DisplayName("Category")]
        public SelectList CategoryList { get; set; }
        public List<Ingredient> IngredientList { get; set; }
    }
}
