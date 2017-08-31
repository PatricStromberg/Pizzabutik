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
    public class DishesController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DishesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Create()
        {
            var categorylist = new SelectList(_applicationDbContext.Categories, "CategoryId", "Name");

            var ingredientList = _applicationDbContext.Ingredients.ToList();

            var model = new CreateViewModel
            {
                CategoryList = categorylist,
                IngredientList = ingredientList
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var newDish = new Dish
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
                DishIngredients = new List<DishIngredient>()
            };
            _applicationDbContext.Add(newDish);
            _applicationDbContext.SaveChanges();

            //_applicationDbContext.Dishes.Add(newDish);
            //_applicationDbContext.SaveChanges();

            //var dish = _applicationDbContext.Dishes.FirstOrDefault(x => x.DishId == newDish.DishId);

            foreach (var ingredient in model.IngredientList)
            {
                if (!ingredient.Enable) continue;

                var newDishIngredient = new DishIngredient
                {
                    Dish = newDish,
                    Ingredient = ingredient
                };
                _applicationDbContext.DishIngredients.Add(newDishIngredient);
                //_applicationDbContext.Add(newDishIngredient);
            }

            _applicationDbContext.SaveChanges();

            return RedirectToAction("Details", "Home");
        }

        //// GET: Dishes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var dish = await _context.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
        //    if (dish == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", dish.CategoryId);
        //    return View(dish);
        //}

        //// POST: Dishes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("DishId,Name,Price,CategoryId")] Dish dish)
        //{
        //    if (id != dish.DishId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(dish);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!DishExists(dish.DishId))
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
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", dish.CategoryId);
        //    return View(dish);
        //}
        
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = _applicationDbContext.Dishes
                .SingleOrDefault(d => d.DishId == id);

            if (dish == null)
            {
                return NotFound();
            }

            _applicationDbContext.Dishes.Remove(dish);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Details", "Home");
        }
    }
}
