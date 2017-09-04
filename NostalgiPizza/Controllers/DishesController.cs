using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var categorylist = new SelectList(_applicationDbContext.Categories, "Id", "Name");

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
                Name = model.NewName,
                Price = model.Price,
                CategoryId = model.CategoryId,
                DishIngredients = new List<DishIngredient>()
            };

            foreach (var ingredient in model.IngredientList)
            {
                Debug.WriteLine($"Ingredient: {ingredient.Id}");
                if (!ingredient.Enable) continue;

                var newDishIngredient = new DishIngredient
                {
                    Dish = newDish,
                    IngredientId = ingredient.Id
                };
                newDish.DishIngredients.Add(newDishIngredient);
            }

            _applicationDbContext.Dishes.Add(newDish);
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
                .SingleOrDefault(d => d.Id == id);

            if (dish == null)
            {
                return NotFound();
            }

            var allIngredients = _applicationDbContext.Ingredients.ToList();

            allIngredients.RemoveAll(x => dish.DishIngredients.Exists(y => y.IngredientId == x.Id));

            var categorylist = new SelectList(_applicationDbContext.Categories, "Id", "Name");

            var model = new EditViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                DishIngredients = dish.DishIngredients,
                CategoryId = dish.CategoryId,
                AllIngredients = allIngredients,
                CategoryList = categorylist
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var dishToUpdate = _applicationDbContext.Dishes.SingleOrDefault(d => d.Id == model.Id);

            dishToUpdate.Name = model.Name;
            dishToUpdate.Price = model.Price;
            dishToUpdate.CategoryId = model.CategoryId;

            var dishIngredientList = new List<DishIngredient>();

            foreach (var dishIngredient in model.DishIngredients.Where(di => di.Ingredient.Enable))
            {
                var newDishIngredient = new DishIngredient
                {
                    Dish = dishToUpdate,
                    IngredientId = dishIngredient.IngredientId
                };
                dishIngredientList.Add(newDishIngredient);
            }

            foreach (var ingredient in model.AllIngredients.Where(ai => ai.Enable))
            {
                var newDishIngredient = new DishIngredient
                {
                    Dish = dishToUpdate,
                    IngredientId = ingredient.Id
                };
                dishIngredientList.Add(newDishIngredient);
            }

            dishToUpdate.DishIngredients = dishIngredientList;
            
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
                .SingleOrDefault(d => d.Id == id);

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
