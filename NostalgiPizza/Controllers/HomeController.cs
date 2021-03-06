﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NostalgiPizza.Data;
using NostalgiPizza.Models;

namespace NostalgiPizza.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext applicationDbContext, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var categories = _applicationDbContext.Categories.Include(c => c.Dishes)
                .ThenInclude(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .ToList();
 
            var model = new StartViewModel
            {
                Categories = categories
            };

            return View(model);
        }

        public ActionResult LogIn()
        {
            var model = new LogInViewModel();
            
            return View(model);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logIn)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(logIn.Email, logIn.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            
            return View(logIn);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var newRegister = new ApplicationUser
                {
                    UserName = register.Email,
                    Email = register.Email,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    ShippingAddress = register.ShippingAddress,
                    ShippingCity = register.ShippingCity,
                    ShippingZip = register.ShippingZip,
                    HomePhone = register.HomePhone,
                    HomeEmail = register.Email
                };

                var userResult = await _userManager.CreateAsync(newRegister, register.Password);
                if (userResult.Succeeded)
                {
                    await _signInManager.SignInAsync(newRegister, isPersistent: false);
                    return RedirectToAction("Index");
                }
            }

            return View(register);
        }

        public IActionResult Details()
        {
            var categories = _applicationDbContext.Categories.ToList();

            var dishes = _applicationDbContext.Dishes.Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .ToList();

            var ingredients = _applicationDbContext.Ingredients.ToList();

            var orders = _applicationDbContext.Orders
                .Include(o => o.ApplicationUser)
                .Include(o => o.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.Dish)
                .ToList();

            var model = new DetailsViewModel
            {
                Categories = categories,
                Dishes = dishes,
                Ingredients = ingredients,
                Orders = orders
            };

            return View(model);
        }

    }
}
