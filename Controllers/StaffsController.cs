﻿using System;
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
            return View(ViewAll().ToList());   
        }

        // GET: Staff/Details/5
        public IActionResult Details(int id)
        {
            var staff = SearchById(id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id,Name,Email,Password,Telephone")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                AddNew(staff);
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET:  Staff/Edit/5
        public IActionResult Edit(int id)
        {
            var staff = SearchById(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST:  Staff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Email,Password,Name,Telephone")] Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
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
                        return NotFound();
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

        // GET:  Staff/Delete/5
        public IActionResult Delete(int id)
        {
            var trainee = SearchById(id);
            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }

        // POST:  Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var staff = SearchById(id);
            DeleteModel(id);
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return (SearchById(id) != null);
        }
    }
}
