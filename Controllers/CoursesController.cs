using AcademicManagement.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement.Models;
using AcademicManagement.FactoryMethod;

namespace AcademicManagement.Controllers
{
    public class CoursesController : CoursesFactory
    {
        public CoursesController(AcademicContext context): base(context)
        {
            this.context = context;
        }

        public IActionResult Index([FromQuery(Name = "name")] string name)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                if (name == null)
                {
                    return View(ViewAll());
                }
                else
                {
                    return View(SearchByName(name));
                }
            }
            return RedirectToAction("Index", "NotFound");
        }

        public IActionResult Create()
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var trainees = this.context.Trainees.ToList();
                var categories = this.context.Categories.ToList();
                dynamic models = new ExpandoObject();
                models.Trainees = trainees;
                models.Categories = categories;
                return View(models);
            }
            return RedirectToAction("Index", "NotFound");
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Assign(int[] listId, string Name, int CategoryId, string Description)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var categories = this.context.Categories.ToList();
                var course = new Course();
                course.Name = Name;
                course.Description = Description;
                course.Category = categories.Find(item => item.Id == CategoryId);
                AddNewTraineeCourse(listId, course);
                AddNew(course);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "NotFound");
        }
        public IActionResult Edit(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var course = SearchById(id);
                var categories = this.context.Categories.ToList();
                var trainees = this.context.Trainees.ToList();
                course.TraineeCourses.ToList().ForEach(item =>
                {
                    trainees.Remove(item.Trainee);
                });
                dynamic models = new ExpandoObject();
                models.CourseTrainees = course.TraineeCourses;
                models.Trainees = trainees;
                models.Categories = categories;
                models.Course = course;
                return View(models);
            }
            return RedirectToAction("Index", "NotFound");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int courseId, int[] listId, string Name, int CategoryId, string Description)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var course = this.context.Courses.ToList().Find(item => item.Id == courseId);
                var categories = this.context.Categories.ToList();
                DeleteTraineeCourses(courseId);
                AddNewTraineeCourse(listId, course);
                course.Name = Name;
                course.Category = categories.Find(item => item.Id == CategoryId);
                course.Description = Description;
                EditModel(course);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "NotFound");
        }
        public IActionResult Details(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var course = SearchById(id);
                return View(course);
            }
            return RedirectToAction("Index", "NotFound");
        }

        public IActionResult Delete(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                var course = SearchById(id);
                return View(course);
            }
            return RedirectToAction("Index", "NotFound");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (UserSingleton.getIntance().Role == new Staff().GetType().Name)
            {
                DeleteModel(id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "NotFound");
        }
    }
}
