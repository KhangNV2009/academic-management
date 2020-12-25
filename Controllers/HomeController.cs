using AcademicManagement.Data;
using AcademicManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly AcademicContext context;

        public HomeController(AcademicContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            ViewBag.IsLoginFail = "hidden";
            ViewBag.IsEmptyField = "";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string role, string email, string password)
        {
            if(email == null || password == null)
            {
                ViewBag.IsEmptyField = "is-invalid";
            }
            else
            {
                if (role == new Admin().GetType().Name)
                {
                    try
                    {
                        var admin = context.Admins.Where(admin => admin.Email == email && admin.Password == password).Single();
                        UserSingleton.getIntance().Email = admin.Email;
                        UserSingleton.getIntance().Role = role;
                        return RedirectToAction("Index", "Admins");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        ViewBag.IsLoginFail = "";
                        return View();
                    }
                }
                if (role == new Staff().GetType().Name)
                {
                    try
                    {
                        var staff = context.Staffs.Where(staff => staff.Email == email && staff.Password == password).Single();
                        UserSingleton.getIntance().Email = staff.Email;
                        UserSingleton.getIntance().Role = role;
                        return RedirectToAction("Index", "Trainers");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        ViewBag.IsLoginFail = "";
                        return View();
                    }
                }
                if (role == new Trainer().GetType().Name)
                {
                    try
                    {
                        var trainer = context.Trainers.Where(trainer => trainer.Email == email && trainer.Password == password).Single();
                        UserSingleton.getIntance().Email = trainer.Email;
                        UserSingleton.getIntance().Role = role;
                        return RedirectToAction("Index", "TrainerAccount");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        ViewBag.IsLoginFail = "";
                        return View();
                    }

                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
