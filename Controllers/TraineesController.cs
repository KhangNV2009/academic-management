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
        public IActionResult Index([FromQuery(Name = "name")] string name)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                if(name == null)
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

        // GET: Trainees/Details/5
        public IActionResult Details(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id,Name,Age,Email,Password,Telephone,DateOfBirth,Education,MainProgrammingLanguage,TOEICScore,ExperienceDetails,Department,Location")] Trainee trainee)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                if (ModelState.IsValid)
                {
                    AddNew(trainee);
                    return RedirectToAction(nameof(Index));
                }
                return View(trainee);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Trainees/Edit/5
        public IActionResult Edit(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
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

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Age,DateOfBirth,Education,MainProgrammingLanguage,TOEICScore,ExperienceDetails,Department,Location,Id,Email,Password,Name,Telephone")] Trainee trainee)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                if (id != trainee.Id)
                {
                    return RedirectToAction("Index", "NotFound");
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
                            return RedirectToAction("Index", "NotFound");
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
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Trainees/Delete/5
        public IActionResult Delete(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
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

        // POST: Trainees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var trainee = SearchById(id);
                DeleteModel(id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "NotFound");
        }

        private bool TraineeExists(int id)
        {
            return (SearchById(id) != null);
        }
    }
}
