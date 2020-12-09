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
    public class TraineesController : TraineesFactory
    {
        public TraineesController(AcademicContext context) : base(context) {
            base.context = context;
        }

        // GET: Trainees
        public IActionResult Index()
        {
            return View(ViewAll().ToList());
        }

        // GET: Trainees/Details/5
        public IActionResult Details(int id)
        {
            var trainee = SearchById(id);
            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id,Name,Age,Email,Password,Telephone,DateOfBirth,Education,MainProgrammingLanguage,TOEICScore,ExperienceDetails,Department,Location")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                AddNew(trainee);
                return RedirectToAction(nameof(Index));
            }
            return View(trainee);
        }

        // GET: Trainees/Edit/5
        public IActionResult Edit(int id)
        {
            var trainee = SearchById(id);
            if (trainee == null)
            {
                return NotFound();
            }
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Age,DateOfBirth,Education,MainProgrammingLanguage,TOEICScore,ExperienceDetails,Department,Location,Id,Email,Password,Name,Telephone")] Trainee trainee)
        {
            if (id != trainee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    EditModel(trainee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TraineeExists(trainee.Id))
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
            return View(trainee);
        }

        // GET: Trainees/Delete/5
        public IActionResult Delete(int id)
        {
            var trainee = SearchById(id);
            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }

        // POST: Trainees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var trainee = SearchById(id);
            DeleteModel(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TraineeExists(int id)
        {
            return (SearchById(id) != null);
        }
    }
}
