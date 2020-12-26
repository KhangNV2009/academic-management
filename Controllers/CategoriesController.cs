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
        public IActionResult Index([FromQuery(Name = "name")] string name)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                if (name == null)
                {
                    return View(ViewAll().ToList());
                }
                else
                {
                    return View(SearchByName(name));
                } 
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Category/Details/5
        public IActionResult Details(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var category = SearchById(id);
                if (category == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(category);
            }
            return RedirectToAction("Index", "NotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id,Name,Description")] Category category)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                if (ModelState.IsValid)
                {
                    AddNew(category);
                    return RedirectToAction(nameof(Index));
                }
                return View(category);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET:  Category/Edit/5
        public IActionResult Edit(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var category = SearchById(id);
                if (category == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(category);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // POST:  Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                if (id != category.Id)
                {
                    return RedirectToAction("Index", "NotFound");
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
                            return RedirectToAction("Index", "NotFound");
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
            return RedirectToAction("Index", "NotFound");
        }

        private bool CategoryExists(int id)
        {
            return (SearchById(id) != null);
        }

        // GET:  Category/Delete/5
        public IActionResult Delete(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var category = SearchById(id);
                if (category == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(category);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // POST:  Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var category = SearchById(id);
                DeleteModel(id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "NotFound");
        }
    }
}
