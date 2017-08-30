using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NostalgiPizza.Models
{
    public class CreateViewModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public SelectList CategoryList { get; set; }
        public List<Ingredient> IngredientList { get; set; }
    }
}
