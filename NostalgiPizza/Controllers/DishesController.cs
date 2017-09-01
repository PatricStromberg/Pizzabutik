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

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = _applicationDbContext.Dishes.Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .SingleOrDefault(d => d.DishId == id);

            if (dish == null)
            {
                return NotFound();
            }

            var dishIngredients = dish.DishIngredients.ToList();

            var allIngredients = _applicationDbContext.Ingredients.ToList();

            allIngredients.RemoveAll(x => dishIngredients.Exists(y => y.IngredientId == x.IngredientId));

            var model = new EditViewModel
            {
                Dish = dish,
                DishIngredients = dishIngredients,
                AllIngredients = allIngredients
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var dishToUpdate = _applicationDbContext.Dishes.SingleOrDefault(d => d.DishId == model.Dish.DishId);
            dishToUpdate.DishIngredients = new List<DishIngredient>();

            foreach (var di in model.DishIngredients)
            {
                if (di.Ingredient.Enable)
                {
                    dishToUpdate.DishIngredients.Add(di);
                }
            }

            foreach (var ai in model.AllIngredients)
            {
                if (ai.Enable)
                {
                    var newDishIngredient = new DishIngredient
                    {
                        Dish = model.Dish,
                        Ingredient = ai
                    };
                    dishToUpdate.DishIngredients.Add(newDishIngredient);
                }
            }

            _applicationDbContext.Update(dishToUpdate);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Details", "Home");
        }

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
