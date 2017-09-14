using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NostalgiPizza.Data;
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

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void All_Are_Sorted()
        {
            //var _ingredients = _serviceProvider.GetService<IngredientService>();
            //var ings = _ingredients.All();

            //Assert.Equal(ings.Count, 0);
        }
    }
}
