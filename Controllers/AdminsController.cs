using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement.Data;
using AcademicManagement.FactoryMethod;
using AcademicManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagement.Controllers
{
    public class AdminsController : AdminsFactory
    {
        public AdminsController(AcademicContext context): base(context)
        {
            base.context = context;
        }
        // GET: Admin
        public IActionResult Index()
        {
            if(UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                return View(ViewAll().ToList());
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Admin/Details/5
        public IActionResult Details(int id)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var Admin = SearchById(id);
                if (Admin == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(Admin);
            }
            return RedirectToAction("Index", "NotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id,Name,Email,Password,Telephone")] Admin Admin)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                if (ModelState.IsValid)
                {
                    AddNew(Admin);
                    return RedirectToAction(nameof(Index));
                }
                return View(Admin);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET:  Admin/Edit/5
        public IActionResult Edit(int id)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var Admin = SearchById(id);
                if (Admin == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(Admin);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST:  Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Email,Password,Name,Telephone")] Admin Admin)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                if (id != Admin.Id)
                {
                    return RedirectToAction("Index", "NotFound");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        EditModel(Admin);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AdminExists(Admin.Id))
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
                return View(Admin);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET:  Admin/Delete/5
        public IActionResult Delete(int id)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var trainee = SearchById(id);
                if (trainee == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }

                return View(trainee);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST:  Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var Admin = SearchById(id);
                DeleteModel(id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return (SearchById(id) != null);
        }
    }
}