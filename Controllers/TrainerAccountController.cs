using AcademicManagement.Data;
using AcademicManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace AcademicManagement.Controllers
{
	public class TrainerAccountController : Controller
	{
		private readonly AcademicContext _context;
        private IWebHostEnvironment _hostingEnvironment;

        public TrainerAccountController(AcademicContext context, IWebHostEnvironment environment)
		{
			_context = context;
            _hostingEnvironment = environment;
		}
        public IActionResult Index()
        {
            if (UserSingleton.getIntance().Role == new Trainer().GetType().Name)
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "avatars/" + UserSingleton.getIntance().Email + ".jpg");
                ViewBag.IsUpload = System.IO.File.Exists(path);

                var email = UserSingleton.getIntance().Email;
                var trainer = _context.Trainers.ToList().Find(m => m.Email == email);
                if (trainer != null)
                {
                    ViewBag.TrainerId = trainer.Id;
                    var courses = _context.Courses.ToList();
                    List<Course> listCourses = new List<Course>();
                    courses.ForEach(item =>
                    {
                        if (SearchCourseById(item.Id).Topics != null)
                        {
                            SearchCourseById(item.Id).Topics.ToList().ForEach(topic =>
                            {
                                if(topic.Trainer.Id == trainer.Id)
                                {
                                    if(listCourses.Find(c => c.Id == item.Id) == null)
                                    {
                                        listCourses.Add(SearchCourseById(item.Id));
                                    }
                                }
                            });
                        }
                    });

                    return View(listCourses);
                }
                return View();
            }
            return RedirectToAction("Index", "NotFound");
        }
        public IActionResult Profile()
		{
			if(UserSingleton.getIntance().Role == new Trainer().GetType().Name)
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "avatars/" + UserSingleton.getIntance().Email + ".jpg");
                ViewBag.IsUpload = System.IO.File.Exists(path);

                var email = UserSingleton.getIntance().Email;
                var trainer = _context.Trainers.ToList().Find(m => m.Email == email);
                if (trainer != null)
                {
                    return View(trainer);
                }
                return View();
            }
            return RedirectToAction("Index", "NotFound");
        }
        public IActionResult Edit()
        {
            if (UserSingleton.getIntance().Role == new Trainer().GetType().Name)
            {
                var email = UserSingleton.getIntance().Email;
                var currentTrainer = _context.Trainers.ToList().Find(e => e.Email == email);
                return View(currentTrainer);
            }
            return RedirectToAction("Index", "NotFound");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IFormFile file, [Bind("Id,Name,Email,Password,Telephone,WorkingPlace,Type")] Trainer trainer)
        {
            if (UserSingleton.getIntance().Role == new Trainer().GetType().Name)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (file != null)
                        {
                            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "avatars");

                            if (file.Length > 0)
                            {
                                string fileName = UserSingleton.getIntance().Email + ".jpg";
                                string filePath = Path.Combine(uploads, fileName);
                                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    file.CopyTo(fileStream);
                                }
                            }
                        }
                        //var email = UserSingleton.getIntance().Email;
                        //var id = _context.Trainers.ToList().Find(e => e.Email == email).Id;
                        //trainer.Id = id;
                        _context.Update(trainer);  
                        _context.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TrainerExists())
                        {
                            return RedirectToAction("Index", "NotFound");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Profile));
                }
                return View(trainer);
            }
            return RedirectToAction("Index", "NotFound");
        }
        private bool TrainerExists()
        {
            var email = UserSingleton.getIntance().Email;
            return (_context.Trainers.ToList().Find(e => e.Email == email) != null);
        }
        public Course SearchCourseById(int id)
        {
            var courseTrainees = _context.CourseTrainees.ToList().FindAll(item => item.CourseId == id);
            var topics = _context.Topics.ToList();
            var trainees = _context.Trainees.ToList();
            var trainers = _context.Trainers.ToList();
            var course = _context.Courses.ToList().Find(item => item.Id == id);
            var category = _context.Categories.ToList().Find(item => item.Id == course.Category.Id);

            if (course.Topics != null)
            {
                course.Topics.ToList().ForEach(item =>
                {
                    item = topics.Find(topic => topic.Id == item.Id);
                    item.Trainer = trainers.Find(trainer => trainer.Id == item.Trainer.Id);
                });
            }

            courseTrainees.ForEach(item =>
            {
                var traineeInCourse = trainees.Find(trainee => trainee.Id == item.TraineeId);
                item.Trainee = traineeInCourse;
            });
            course.TraineeCourses = courseTrainees;
            course.Category = category;
            return course;
        }
    }
}
