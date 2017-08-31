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

        //// POST: Ingredients/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IngredientId,Name,Enable")] Ingredient ingredient)
        //{
        //    if (id != ingredient.IngredientId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(ingredient);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!IngredientExists(ingredient.IngredientId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(ingredient);
        //}
        
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
