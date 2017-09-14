using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using NostalgiPizza.Data;
using NostalgiPizza.Models;

namespace NostalgiPizza.Services
{
    public class IngredientService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IngredientService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<Ingredient> All()
        {
            return _applicationDbContext.Ingredients.OrderBy(i => i.Name).ToList();
        }
    }
}
