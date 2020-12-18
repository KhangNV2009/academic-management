using AcademicManagement.Data;
using AcademicManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicManagement.Controllers
{
	public class UserProfileController : Controller
	{
		private readonly AcademicContext _context;

		public UserProfileController(AcademicContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var email = UserSingleton.getIntance().Email;
            var trainer = _context.Trainers.First(m => m.Email == email);
            if (trainer != null)
            {
                return View(trainer);
            }
            return View();
		}
		public IActionResult Edit()
		{
			var email = UserSingleton.getIntance().Email;
			var trainer = _context.Trainers.First(m => m.Email == email);
            if (trainer != null)
            {
                return View(trainer);
            }
            return View();
		}
	}
}
