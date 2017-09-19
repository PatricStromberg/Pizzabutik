using System;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NostalgiPizza.Controllers;
using NostalgiPizza.Data;
using NostalgiPizza.Models;
using NostalgiPizza.Services;
using Xunit;

namespace PizzaTests
{
    public class IngredientsServiceTest
    {
        private readonly IServiceProvider _serviceProvider;

        public IngredientsServiceTest()
        {
            var efServiceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(b => b
                .UseInMemoryDatabase("Pizzadatabas")
                .UseInternalServiceProvider(efServiceProvider));
            services.AddTransient<IngredientService>();
            services.AddTransient<IngredientsController>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void All_Ingredients()
        {
            var context = _serviceProvider.GetService<ApplicationDbContext>();

            var service = _serviceProvider.GetService<IngredientService>();
            var ingredients = service.All();

            Assert.Collection(ingredients);
        }

        [Fact]
        public void agag()
        {
            
        }


    }
}
