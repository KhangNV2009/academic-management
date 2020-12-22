using AcademicManagement.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement.Models;

namespace AcademicManagement.Controllers
{
    public class CoursesController : Controller
    {
        public AcademicContext context;

        public CoursesController(AcademicContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var courses = this.context.Courses.ToList();
            var topics = this.context.Topics.ToList();
            var trainers = this.context.Trainers.ToList();
           
            courses.ForEach(course =>
            {
                var currentTopic = topics.Find(item => item.Course.Id == course.Id);
                if(currentTopic != null)
                {
                    var currentTrainer = trainers.Find(item => item.Id == currentTopic.Trainer.Id);
                    if (currentTrainer != null)
                    {
                        currentTopic.Trainer = currentTrainer;
                    }
                    course.Topics.Add(currentTopic);
                }
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
            var trainees = this.context.Trainees.ToList();
            var traineesCourses = this.context.CourseTrainees.ToList();

            var course = new Course();
            course.Name = Name;
            course.Description = Description;
            course.Category = categories.Find(item => item.Id == CategoryId);
            
            foreach(var id in listId)
            {
                TraineeCourse traineeCourse = new TraineeCourse();
                traineeCourse.Course = course;
                traineeCourse.Trainee = trainees.Find(item => item.Id == id);
                this.context.Add(traineeCourse);
            }
            this.context.Add(course);
            this.context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var currentCourse = this.context.Courses.ToList().Find(item => item.Id == id);
            var categories = this.context.Categories.ToList();
            var trainees = this.context.Trainees.ToList();
            var courseTrainees = this.context.CourseTrainees.ToList().FindAll(item => item.CourseId == id);

            courseTrainees.ForEach(item =>
            {
                var traineeInCourse = trainees.Find(trainee => trainee.Id == item.TraineeId);
                item.Trainee = traineeInCourse;
                trainees.Remove(traineeInCourse);
            });


            dynamic models = new ExpandoObject();
            models.CourseTrainees = courseTrainees;
            models.Trainees = trainees;
            models.Categories = categories;
            models.Course = currentCourse;

            return View(models);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int courseId, int[] listId, string Name, int CategoryId, string Description)
        {
            var course = this.context.Courses.ToList().Find(item => item.Id == courseId);
            var categories = this.context.Categories.ToList();
            var trainees = this.context.Trainees.ToList();
            var currentTraineeCourses = this.context.CourseTrainees.ToList().FindAll(item => item.CourseId == courseId);

            if(currentTraineeCourses != null)
            {
                currentTraineeCourses.ForEach(item =>
                {
                    this.context.CourseTrainees.Remove(item);
                    course.TraineeCourses.Remove(item);
                });
            }

            foreach (var id in listId)
            {
                TraineeCourse traineeCourse = new TraineeCourse();
                traineeCourse.Course = course;
                traineeCourse.Trainee = trainees.Find(item => item.Id == id);
                this.context.Add(traineeCourse);
            }

            course.Name = Name;
            course.Category = categories.Find(item => item.Id == CategoryId);
            course.Description = Description;
            this.context.Courses.Update(course);

            this.context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
