using System.Linq;
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

        public IActionResult Index(bool backToMenu = false)
        {
            var categories = _applicationDbContext.Categories.Include(c => c.Dishes)
                .ThenInclude(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .ToList();
 
            var model = new StartViewModel
            {
                Categories = categories
            };

            if (backToMenu)
            {
                model.BackToMenu = "menu";
            }
            return View(model);
        }

        public ActionResult LogInOrRegister()
        {
            var model = new LoginOrRegisterViewModel
            {
                LogInViewModel = new LogInViewModel(),
                RegisterViewModel = new RegisterViewModel()
            };
            return View(model);
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

            var model = new LoginOrRegisterViewModel
            {
                LogInViewModel = logIn,
                RegisterViewModel = new RegisterViewModel()
            };

            return View("LogInOrRegister", model);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
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
                    HomePhone = register.HomePhone
                };

                var userResult = await _userManager.CreateAsync(newRegister, register.Password);
                if (userResult.Succeeded)
                {
                    await _signInManager.SignInAsync(newRegister, isPersistent: false);
                    return RedirectToAction("Index");
                }
            }
            var model = new LoginOrRegisterViewModel
            {
                LogInViewModel = new LogInViewModel(),
                RegisterViewModel = register
            };
            return View("LogInOrRegister", model);
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
