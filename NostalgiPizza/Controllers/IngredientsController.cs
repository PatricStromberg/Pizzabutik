using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
                Name = model.NewName
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

            var ingredient = _applicationDbContext.Ingredients.SingleOrDefault(m => m.Id == id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var model = new EditViewModel
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var ingredient = _applicationDbContext.Ingredients.SingleOrDefault(i => i.Id == model.Id);

            ingredient.Name = model.Name;

            //_applicationDbContext.Update(ingredient);
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
                .SingleOrDefault(i => i.Id == id);

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
