using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using NostalgiPizza.Models;

namespace NostalgiPizza.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UserManager<ApplicationUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            var aUser = new ApplicationUser
            {
                UserName = "student@test.com",
                Email = "student@test.com",
                FirstName = "Patric",
                LastName = "Strömberg",
                ShippingAddress = "Eskadervägen 40",
                ShippingCity = "Täby",
                ShippingZip = 18358,
                HomeEmail = "stromberg.patric@hotmail.com",
                HomePhone = 0731234567,
                CardNumber = 1234567891011121,
                ExpirationMonth = 10,
                ExpirationYear = 17,
                CVS = 123
            };
            var userResult = userManager.CreateAsync(aUser, "Pa$$w0rd").Result;

            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            var adminUser = new ApplicationUser
            {
                UserName = "admin@test.com",
                Email = "admin@test.com",
                FirstName = "Patric",
                LastName = "Strömberg",
                ShippingAddress = "Eskadervägen 40",
                ShippingCity = "Täby",
                ShippingZip = 18358,
                HomeEmail = "stromberg.patric@hotmail.com",
                HomePhone = 0731234567,
                CardNumber = 1234567891011121,
                ExpirationMonth = 10,
                ExpirationYear = 17,
                CVS = 123
            };
            var adminUserResult = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;

            var roleAddedResult = userManager.AddToRoleAsync(adminUser, "Admin").Result;

            if (!context.Dishes.Any())
            {
                //Ingredients
                var groundBeef = new Ingredient { Name = "ground beef" };
                var crispyRasherBacon = new Ingredient { Name = "crispy rasher bacon" };
                var mushroom = new Ingredient { Name = "mushroom" };
                var pepperoni = new Ingredient { Name = "pepperoni" };
                var italianSausage = new Ingredient { Name = "italian sausage" };
                var freshBabySpinach = new Ingredient { Name = "fresh baby spinach" };
                var smokedLegHam = new Ingredient { Name = "smoked leg ham" };
                var pineapple = new Ingredient { Name = "pineapple" };
                var toppedWithOregano = new Ingredient { Name = "topped with oregano" };
                var tomatoCapsicumSauce = new Ingredient { Name = "tomato capsicum sauce" };
                var springOnion = new Ingredient { Name = "spring onion" };
                var toppedWithHickoryBBQSauce = new Ingredient { Name = "topped with Hickory BBQ sauce" };
                var succulentChicken = new Ingredient { Name = "succulent chicken" };
                var porkAndFennelSausage = new Ingredient { Name = "pork & fennel sausage" };
                var lotsOfStretchyMozzarellaCheese = new Ingredient { Name = "lots of stretchy mozzarella cheese" };
                var redOnion = new Ingredient { Name = "red onion" };
                var lateHarvestJalapenos = new Ingredient { Name = "late harvest jalapenos" };
                var BBQSauceBase = new Ingredient { Name = "BBQ sauce base" };
                var freshTomato = new Ingredient { Name = "fresh tomato" };
                var toppedWithChilliFlakes = new Ingredient { Name = "topped with chilli flakes" };
                var toppedWithMayonnaise = new Ingredient { Name = "topped with mayonnaise" };

                //Dishes
                var loadedSupreme = new Dish { Name = "Loaded Supreme", Price = 120 };
                var cheesyBaconHawaiian = new Dish { Name = "Cheesy Bacon Hawaiian", Price = 110 };
                var megaMeatlovers = new Dish { Name = "Mega Meatlover", Price = 120 };
                var spicyBBQPorkAndBacon = new Dish { Name = "Spicy BBQ Pork and Bacon", Price = 120 };
                var doubleBaconCheeseburger = new Dish { Name = "Double Bacon Cheeseburger", Price = 110 };
                var fireBreather = new Dish { Name = "Fire Breather", Price = 110 };
                

                //DishIngredients
                var loadedSupremeGroundBeef = new DishIngredient { Dish = loadedSupreme, Ingredient = groundBeef };
                var loadedSupremeCrispyRasherBacon = new DishIngredient { Dish = loadedSupreme, Ingredient = crispyRasherBacon };
                var loadedSupremeMushroom = new DishIngredient { Dish = loadedSupreme, Ingredient = mushroom };
                var loadedSupremePepperoni = new DishIngredient { Dish = loadedSupreme, Ingredient = pepperoni };
                var loadedSupremeItalianSausage = new DishIngredient { Dish = loadedSupreme, Ingredient = italianSausage };
                var loadedSupremeFreshBabySpinach = new DishIngredient { Dish = loadedSupreme, Ingredient = freshBabySpinach };
                var loadedSupremeSmokedLegHam = new DishIngredient { Dish = loadedSupreme, Ingredient = smokedLegHam };
                var loadedSupremePineapple = new DishIngredient { Dish = loadedSupreme, Ingredient = pineapple };
                var loadedSupremeToppedWithOregano = new DishIngredient { Dish = loadedSupreme, Ingredient = toppedWithOregano };
                var loadedSupremeTomatoCapsicumSauce = new DishIngredient { Dish = loadedSupreme, Ingredient = tomatoCapsicumSauce };
                var loadedSupremeSpringOnion = new DishIngredient { Dish = loadedSupreme, Ingredient = springOnion };

                var cheesyBaconHawaiianCrispyRasherBacon = new DishIngredient { Dish = cheesyBaconHawaiian, Ingredient = crispyRasherBacon };
                var cheesyBaconHawaiianSmokedLegHam = new DishIngredient { Dish = cheesyBaconHawaiian, Ingredient = smokedLegHam };
                var cheesyBaconHawaiianPineapple = new DishIngredient { Dish = cheesyBaconHawaiian, Ingredient = pineapple };
                var cheesyBaconHawaiianToppedWithHickoryBBQSauce = new DishIngredient { Dish = cheesyBaconHawaiian, Ingredient = toppedWithHickoryBBQSauce };

                var megaMeatloversSuccullentChicken = new DishIngredient { Dish = megaMeatlovers, Ingredient = succulentChicken };
                var megaMeatloversItalianSausage = new DishIngredient { Dish = megaMeatlovers, Ingredient = italianSausage };
                var megaMeatloversCrispyRasherBacon = new DishIngredient { Dish = megaMeatlovers, Ingredient = crispyRasherBacon };
                var megaMeatloversGroundBeef = new DishIngredient { Dish = megaMeatlovers, Ingredient = groundBeef };
                var megaMeatloversPepperoni = new DishIngredient { Dish = megaMeatlovers, Ingredient = pepperoni };
                var megaMeatloversPorkAndFennelSausage = new DishIngredient { Dish = megaMeatlovers, Ingredient = porkAndFennelSausage };
                var megaMeatloversToppedWithHickoryBBQSauce = new DishIngredient { Dish = megaMeatlovers, Ingredient = toppedWithHickoryBBQSauce };

                var spicyBBQPorkAndBaconLotsOfStretchyMozzarellaCheese = new DishIngredient { Dish = spicyBBQPorkAndBacon, Ingredient = lotsOfStretchyMozzarellaCheese };
                var spicyBBQPorkAndBaconPorkAndFennelSausage = new DishIngredient { Dish = spicyBBQPorkAndBacon, Ingredient = porkAndFennelSausage };
                var spicyBBQPorkAndBaconCrispyRasherBacon = new DishIngredient { Dish = spicyBBQPorkAndBacon, Ingredient = crispyRasherBacon };
                var spicyBBQPorkAndBaconPineapple = new DishIngredient { Dish = spicyBBQPorkAndBacon, Ingredient = pineapple };
                var spicyBBQPorkAndBaconRedOnion = new DishIngredient { Dish = spicyBBQPorkAndBacon, Ingredient = redOnion };
                var spicyBBQPorkAndBaconLateHarvestJalapenos = new DishIngredient { Dish = spicyBBQPorkAndBacon, Ingredient = lateHarvestJalapenos };
                var spicyBBQPorkAndBaconBBQSauceBase = new DishIngredient { Dish = spicyBBQPorkAndBacon, Ingredient = BBQSauceBase };
                var spicyBBQPorkAndBaconSpringOnion = new DishIngredient { Dish = spicyBBQPorkAndBacon, Ingredient = springOnion };

                var fireBreatherPorkAndFennelSausage = new DishIngredient { Dish = fireBreather, Ingredient = porkAndFennelSausage };
                var fireBreatherGroundBeef = new DishIngredient { Dish = fireBreather, Ingredient = groundBeef };
                var fireBreatherPepperoni = new DishIngredient { Dish = fireBreather, Ingredient = pepperoni };
                var fireBreatherLateHarvestJalapenos = new DishIngredient { Dish = fireBreather, Ingredient = lateHarvestJalapenos };
                var fireBreatherFreshTomato = new DishIngredient { Dish = fireBreather, Ingredient = freshTomato };
                var fireBreatherRedOnion = new DishIngredient { Dish = fireBreather, Ingredient = redOnion };
                var fireBreatherToppedWithChilliFlakes = new DishIngredient { Dish = fireBreather, Ingredient = toppedWithChilliFlakes };

                var doubleBaconCheeseburgerGroundBeef = new DishIngredient { Dish = doubleBaconCheeseburger, Ingredient = groundBeef };
                var doubleBaconCheeseburgerCrispyRasherBacon = new DishIngredient { Dish = doubleBaconCheeseburger, Ingredient = crispyRasherBacon };
                var doubleBaconCheeseburgerBBQSauceBase = new DishIngredient { Dish = doubleBaconCheeseburger, Ingredient = BBQSauceBase };
                var doubleBaconCheeseburgerToppedWithMayonnaise = new DishIngredient { Dish = doubleBaconCheeseburger, Ingredient = toppedWithMayonnaise };


                //DishIngredientsList
                loadedSupreme.DishIngredients = new List<DishIngredient>
                {
                    loadedSupremeGroundBeef,
                    loadedSupremeCrispyRasherBacon,
                    loadedSupremeMushroom,
                    loadedSupremePepperoni,
                    loadedSupremeItalianSausage,
                    loadedSupremeFreshBabySpinach,
                    loadedSupremeSmokedLegHam,
                    loadedSupremePineapple,
                    loadedSupremeToppedWithOregano,
                    loadedSupremeTomatoCapsicumSauce,
                    loadedSupremeSpringOnion
                };
                cheesyBaconHawaiian.DishIngredients = new List<DishIngredient>
                {
                    cheesyBaconHawaiianCrispyRasherBacon,
                    cheesyBaconHawaiianSmokedLegHam,
                    cheesyBaconHawaiianPineapple,
                    cheesyBaconHawaiianToppedWithHickoryBBQSauce
                };
                megaMeatlovers.DishIngredients = new List<DishIngredient>
                {
                    megaMeatloversSuccullentChicken,
                    megaMeatloversItalianSausage,
                    megaMeatloversCrispyRasherBacon,
                    megaMeatloversGroundBeef,
                    megaMeatloversPepperoni,
                    megaMeatloversPorkAndFennelSausage,
                    megaMeatloversToppedWithHickoryBBQSauce
                };
                spicyBBQPorkAndBacon.DishIngredients = new List<DishIngredient>
                {
                    spicyBBQPorkAndBaconLotsOfStretchyMozzarellaCheese,
                    spicyBBQPorkAndBaconPorkAndFennelSausage,
                    spicyBBQPorkAndBaconCrispyRasherBacon,
                    spicyBBQPorkAndBaconPineapple,
                    spicyBBQPorkAndBaconRedOnion,
                    spicyBBQPorkAndBaconLateHarvestJalapenos,
                    spicyBBQPorkAndBaconBBQSauceBase,
                    spicyBBQPorkAndBaconSpringOnion
                };
                fireBreather.DishIngredients = new List<DishIngredient>
                {
                    fireBreatherPorkAndFennelSausage,
                    fireBreatherGroundBeef,
                    fireBreatherPepperoni,
                    fireBreatherLateHarvestJalapenos,
                    fireBreatherFreshTomato,
                    fireBreatherRedOnion,
                    fireBreatherToppedWithChilliFlakes
                };
                doubleBaconCheeseburger.DishIngredients = new List<DishIngredient>
                {
                    doubleBaconCheeseburgerGroundBeef,
                    doubleBaconCheeseburgerCrispyRasherBacon,
                    doubleBaconCheeseburgerBBQSauceBase,
                    doubleBaconCheeseburgerToppedWithMayonnaise
                };

                //Category
                var premiumPizza = new Category{ Name = "Premium Pizzas" };
                var traditionalPizza = new Category { Name = "Traditional Pizzas" };
                var glutenFree = new Category{ Name = "Gluten Free" };

                premiumPizza.Dishes = new List<Dish>
                {
                    loadedSupreme,
                    cheesyBaconHawaiian,
                    megaMeatlovers
                };
                traditionalPizza.Dishes = new List<Dish>
                {
                    spicyBBQPorkAndBacon,
                    fireBreather,
                    doubleBaconCheeseburger
                };

                //Add to inMem
                context.AddRange(
                    groundBeef,
                    crispyRasherBacon,
                    mushroom,
                    pepperoni,
                    italianSausage,
                    freshBabySpinach,
                    smokedLegHam,
                    pineapple,               
                    toppedWithOregano,
                    tomatoCapsicumSauce,
                    springOnion,
                    toppedWithHickoryBBQSauce,
                    succulentChicken, 
                    porkAndFennelSausage, lotsOfStretchyMozzarellaCheese,
                    redOnion,
                    lateHarvestJalapenos,
                    BBQSauceBase,
                    freshTomato,
                    toppedWithChilliFlakes,
                    toppedWithMayonnaise);                   
                                         
                context.AddRange(        
                    loadedSupreme,       
                    cheesyBaconHawaiian, 
                    megaMeatlovers,
                    spicyBBQPorkAndBacon,
                    fireBreather,
                    doubleBaconCheeseburger);      

                context.AddRange(
                    premiumPizza,
                    traditionalPizza,
                    glutenFree);

                //Save
                context.SaveChanges();
            }
        }

    }
}
