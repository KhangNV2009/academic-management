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
	public class TopicsController : TopicFactory
	{
        public TopicsController(AcademicContext context) : base(context)
        {
            base.context = context;
        }

        // GET: Topic
        public IActionResult Index()
        {
            return View(ViewAll().ToList());
        }

        // GET: Topic/Details/5
        public IActionResult Details(int id)
        {
            var topic = SearchById(id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id,Name,Decription,Trainer,Course")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                AddNew(topic);
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }

        // GET: Topic/Edit/5
        public IActionResult Edit(int id)
        {
            var topic = SearchById(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        // POST: Topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Decription,Trainer,Course")] Topic topic)
        {
            if (id != topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    EditModel(topic);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TraineeExists(topic.Id))
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
            return View(topic);
        }

        // GET: Topic/Delete/5
        public IActionResult Delete(int id)
        {
            var topic = SearchById(id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        // POST: Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var topic = SearchById(id);
            DeleteModel(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TraineeExists(int id)
        {
            return (SearchById(id) != null);
        }
    }
}
