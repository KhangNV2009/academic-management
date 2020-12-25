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
    public class TrainersController : TrainersFactory
    {
        public TrainersController(AcademicContext context) : base(context)
        {
            base.context = context;
        }

        // GET: Trainers
        public IActionResult Index()
        {
            if(UserSingleton.getIntance().Role == new Staff().GetType().Name || UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                return View(ViewAll().ToList());
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Trainers/Details/5
        public IActionResult Details(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name || UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var trainer = SearchById(id);
                if (trainer == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(trainer);
            }
            return RedirectToAction("Index","NotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id,Name,Email,Password,Telephone,WorkingPlace,Type")] Trainer trainer)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name || UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                if (ModelState.IsValid)
                {
                    AddNew(trainer);
                    return RedirectToAction(nameof(Index));
                }
                return View(trainer);   
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Trainers/Edit/5
        public IActionResult Edit(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name || UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var trainer = SearchById(id);
                if (trainer == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(trainer);
            }
            return RedirectToAction("Index", "NotFound");      
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Email,Password,Telephone,WorkingPlace,Type")] Trainer trainer)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name || UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                if (id != trainer.Id)
                {
                    return RedirectToAction("Index", "NotFound");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        EditModel(trainer);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TrainerExists(trainer.Id))
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
                return View(trainer);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Trainers/Delete/5
        public IActionResult Delete(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name || UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var trainer = SearchById(id);
                if (trainer == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(trainer);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name || UserSingleton.getIntance().Role == new Admin().GetType().Name)
            {
                var trainer = SearchById(id);
                DeleteModel(id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "NotFound");
        }

        private bool TrainerExists(int id)
        {
            return (SearchById(id) != null);
        }
    }
}
