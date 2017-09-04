using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NostalgiPizza.Data;
using NostalgiPizza.Models;

namespace NostalgiPizza.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoriesController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var newCategory = new Category
            {
                Name = model.NewName
            };

            _applicationDbContext.Add(newCategory);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Details", "Home");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _applicationDbContext.Categories.SingleOrDefault(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new EditViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = _applicationDbContext.Categories.SingleOrDefault(c => c.Id == model.Id);

            category.Name = model.Name;
            
            //_applicationDbContext.Update(category);
            _applicationDbContext.SaveChanges();
                
            return RedirectToAction("Details", "Home");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _applicationDbContext.Categories
                .SingleOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            _applicationDbContext.Categories.Remove(category);
            _applicationDbContext.SaveChanges();

            return RedirectToAction("Details", "Home");
        }
    }
}
