using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NostalgiPizza.Data;
using NostalgiPizza.Models;

namespace NostalgiPizza.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ApplicationDbContext applicationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var categories = _applicationDbContext.Categories.Include(c => c.Dishes)
                .ThenInclude(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .ToList();

            var ingredients = _applicationDbContext.Ingredients.ToList();
            
            var model = new StartViewModel
            {
                Categories = categories
                
            };

            return View(model);
        }

        public ActionResult LogInOrRegister()
        {
            return View();
        }

        public async Task<IActionResult> LogIn(LoginViewModel logIn)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(logIn.Email, logIn.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View("LogInOrRegister", logIn);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Details()
        {
            var dishes = _applicationDbContext.Dishes.Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .ToList();

            var model = new DetailsViewModel
            {
                Dishes = dishes
            };

            return View(model);
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
            _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Details");
        }
    }
}
