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
using System.Dynamic;

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
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var trainers = context.Trainers.ToList();
                var courses = context.Courses.ToList();
                var topics = ViewAll().ToList();
                dynamic models = new ExpandoObject();
                models.Trainers = trainers;
                models.Courses = courses;
                models.Topics = topics;
                return View(models);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Topic/Details/5
        public IActionResult Details(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var topic = SearchById(id);
                if (topic == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(topic);
            }
            return RedirectToAction("Index", "NotFound");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Id,Name,Description,Trainer,Course")] Topic topic, int courseId, int trainerId)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                if (ModelState.IsValid)
                {
                    topic.Trainer = this.context.Trainers.ToList().Find(item => item.Id == trainerId);
                    topic.Course = this.context.Courses.ToList().Find(item => item.Id == courseId);
                    AddNew(topic);
                    return RedirectToAction(nameof(Index));
                }
                return View(topic);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Topic/Edit/5
        public IActionResult Edit(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var topic = SearchById(id);
                var trainers = context.Trainers.ToList();
                var courses = context.Courses.ToList();
                if (topic == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                dynamic models = new ExpandoObject();
                models.Trainers = trainers;
                models.Courses = courses;
                models.Topic = topic;
                return View(models);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // POST: Topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Trainer,Course")] Topic topic, int courseId, int trainerId)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                if (id != topic.Id)
                {
                    return RedirectToAction("Index", "NotFound");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        topic.Course = context.Courses.ToList().Find(item => item.Id == courseId);
                        topic.Trainer = context.Trainers.ToList().Find(item => item.Id == trainerId);
                        EditModel(topic);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TraineeExists(topic.Id))
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
                return View(topic);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // GET: Topic/Delete/5
        public IActionResult Delete(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var topic = SearchById(id);
                if (topic == null)
                {
                    return RedirectToAction("Index", "NotFound");
                }
                return View(topic);
            }
            return RedirectToAction("Index", "NotFound");
        }

        // POST: Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var topic = SearchById(id);
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
