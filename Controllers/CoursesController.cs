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

        public IActionResult Index()
        {
            var courses = this.context.Courses.ToList();
            //var topics = this.context.Topics.ToList();
            //var trainers = this.context.Trainers.ToList();
           
            courses.ForEach(course =>
            {
                course = SearchById(course.Id);
            });
           
            return View(courses);
        }

        public IActionResult Create()
        {
            var trainees = this.context.Trainees.ToList();
            var categories = this.context.Categories.ToList();
            
            dynamic models = new ExpandoObject();
            models.Trainees = trainees;
            models.Categories = categories;
            return View(models);
        }
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Assign(int[] listId, string Name, int CategoryId, string Description)
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
        public IActionResult Edit(int id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int courseId, int[] listId, string Name, int CategoryId, string Description)
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
        public IActionResult Details(int id)
        {
            var course = SearchById(id);
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            var course = SearchById(id);
            return View(course);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            DeleteModel(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
