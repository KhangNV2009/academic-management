using AcademicManagement.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement.FactoryMethod;
using AcademicManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagement.Controllers
{
    public class CategoriesController : CategoriesFactory
    {
        public CategoriesController(AcademicContext context) : base(context)
        {
            base.context = context;
        }

        // GET: Category
        public IActionResult Index()
        {
            return View(ViewAll().ToList());
        }

        // GET: Category/Details/5
        public IActionResult Details(int id)
        {
            var category = SearchById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                AddNew(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET:  Category/Edit/5
        public IActionResult Edit(int id)
        {
            var category = SearchById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST:  Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    EditModel(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        private bool CategoryExists(int id)
        {
            return (SearchById(id) != null);
        }

        // GET:  Category/Delete/5
        public IActionResult Delete(int id)
        {
            var category = SearchById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST:  Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = SearchById(id);
            DeleteModel(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
