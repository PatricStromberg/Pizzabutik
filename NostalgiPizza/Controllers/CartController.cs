﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NostalgiPizza.Data;
using NostalgiPizza.Models;

namespace NostalgiPizza.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CartController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult ViewCart()
        {
            var cartId = HttpContext.Session.GetInt32("Cart");

            if (!cartId.HasValue)
            {
                return View();
            }
            var cart = _applicationDbContext.Carts
                .Include(c => c.CartItems)
                .ThenInclude(x => x.Dish)
                .Include(d => d.CartItems)
                .ThenInclude(x =>x.CartItemIngredients)
                .ThenInclude(ci => ci.Ingredient)
                .FirstOrDefault(x => x.Id.Equals(cartId));

            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var dish = _applicationDbContext.Dishes
                .Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .FirstOrDefault(d => d.Id.Equals(id));

            var allIngredients = _applicationDbContext.Ingredients.OrderBy(x => x.Name).ToList();

            allIngredients.RemoveAll(x => dish.DishIngredients.Exists(y => y.IngredientId == x.Id));

            var model = new AddToCartViewModel
            {
                Dish = dish,
                AllIngredients = allIngredients
            };

            return PartialView("_ModalContent", model);
        }

        [HttpPost]
        public IActionResult AddToCart(AddToCartViewModel model)
        {
            var cartId = HttpContext.Session.GetInt32("Cart");

            Cart cart;

            if (!cartId.HasValue)
            {
                cart = new Cart
                {
                    CartItems = new List<CartItem>()
                };
                _applicationDbContext.Carts.Add(cart);

                HttpContext.Session.SetInt32("Cart", cart.Id);
            }
            else
            {
                cart = _applicationDbContext.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.Id.Equals(cartId));
            }

            var cartItem = new CartItem
            {
                CartId = cart.Id,
                DishId = model.Dish.Id,
                CartItemIngredients = new List<CartItemIngredient>(),
                Quantity = 1
            };

            foreach (var enableIngredient in model.Dish.DishIngredients.Where(x => x.Ingredient.Enable))
            {
                var cartItemIngredient = new CartItemIngredient
                {
                    CartItem = cartItem,
                    IngredientId = enableIngredient.Ingredient.Id
                };
                cartItem.CartItemIngredients.Add(cartItemIngredient);
            }

            foreach (var enableIngredient in model.AllIngredients.Where(x => x.Enable))
            {
                var cartItemIngredient = new CartItemIngredient
                {
                    CartItem = cartItem,
                    IngredientId = enableIngredient.Id
                };
                cartItem.CartItemIngredients.Add(cartItemIngredient);
            }

            cart.CartItems.Add(cartItem);

            _applicationDbContext.SaveChanges();

            return RedirectToAction("Index", "Home", new { backToMenu = true });
        }
    }
}