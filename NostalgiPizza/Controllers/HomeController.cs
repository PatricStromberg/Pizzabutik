using System.Linq;
using System.Threading.Tasks;
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
            var categories = _applicationDbContext.Categories.ToList();

            var dishes = _applicationDbContext.Dishes.Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .ToList();

            var ingredients = _applicationDbContext.Ingredients.ToList();

            var model = new DetailsViewModel
            {
                Categories = categories,
                Dishes = dishes,
                Ingredients = ingredients
            };

            return View(model);
        }

    }
}
