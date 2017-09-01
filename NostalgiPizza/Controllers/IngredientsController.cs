using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NostalgiPizza.Data;
using NostalgiPizza.Models;

namespace NostalgiPizza.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IngredientsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var newIngredient = new Ingredient
            {
                Name = model.Name
            };

            _applicationDbContext.Add(newIngredient);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Details", "Home");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = _applicationDbContext.Ingredients.SingleOrDefault(m => m.IngredientId == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var model = new EditViewModel
            {
                Ingredient = ingredient
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            _applicationDbContext.Update(model.Ingredient);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Details", "Home");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = _applicationDbContext.Ingredients
                .SingleOrDefault(i => i.IngredientId == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            _applicationDbContext.Ingredients.Remove(ingredient);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Details", "Home");
        }
    }
}
