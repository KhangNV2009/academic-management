using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcademicManagement.Data;
using AcademicManagement.Models;
using AcademicManagement.FactoryMethod;

namespace AcademicManagement.Controllers
{
    public class StaffsController : StaffFactory
    {
        public StaffsController(AcademicContext context) : base(context)
        {
            base.context = context;
        }

        // GET: Staff
        public IActionResult Index()
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                return View(ViewAll().ToList());
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Staff/Details/5
        public IActionResult Details(int id)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var staff = SearchById(id);
                if (staff == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(staff);
            }
            return RedirectToAction("Index", "NotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id,Name,Email,Password,Telephone")] Staff staff)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                if (ModelState.IsValid)
                {
                    AddNew(staff);
                    return RedirectToAction(nameof(Index));
                }
                return View(staff);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET:  Staff/Edit/5
        public IActionResult Edit(int id)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var staff = SearchById(id);
                if (staff == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(staff);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // POST:  Staff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Email,Password,Name,Telephone")] Staff staff)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                if (id != staff.Id)
                {
                    return RedirectToAction("Index", "NotFound");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        EditModel(staff);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!StaffExists(staff.Id))
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
                return View(staff);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET:  Staff/Delete/5
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
            return RedirectToAction("Index", "NotFound");
        }

        // POST:  Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var staff = SearchById(id);
                DeleteModel(id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "NotFound");
        }

        private bool StaffExists(int id)
        {
            return (SearchById(id) != null);
        }
    }
}
