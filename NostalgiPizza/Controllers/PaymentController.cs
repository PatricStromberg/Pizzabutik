using System;
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
    public class PaymentController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public PaymentController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ActionResult> Payment(int id)
        {
            var isSignedIn = _signInManager.IsSignedIn(User);
            var currentUser = await _userManager.GetUserAsync(User);

            var cart = _applicationDbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.CartItemIngredients)
                .ThenInclude(cii => cii.Ingredient)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Dish)
                .FirstOrDefault(c => c.Id.Equals(id));

            var model = new PaymentViewModel
            {
                Cart = cart
            };

            if (!isSignedIn) return View(model);

            model.CurrentUser = currentUser;

            return View(model);
        }

        [HttpPost]
        public IActionResult Payment(PaymentViewModel model)
        {
            var paymentUser = model.CurrentUser;

            _applicationDbContext.ApplicationUsers.Add(paymentUser);

            var order = new Order
            {
                CartId = model.Cart.Id,
                ApplicationUser = model.CurrentUser
            };

            _applicationDbContext.Orders.Add(order);
            _applicationDbContext.SaveChanges();

            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index", "Home");
        }
    }
}
