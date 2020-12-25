using AcademicManagement.Data;
using AcademicManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                    return RedirectToAction(nameof(Index));
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
    }
}
