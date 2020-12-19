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
            return View();
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
        public IActionResult Assign(int[] listId, string Name, string Category, string Desciption)
        {
            Console.WriteLine(Name);
            Console.WriteLine(Category);
            Console.WriteLine(Desciption);

            foreach(int id in listId)
            {
                Console.WriteLine(id);
            }
            var trainees = this.context.Trainees.ToList();
            var categories = this.context.Categories.ToList();

            dynamic models = new ExpandoObject();
            models.Trainees = trainees;
            models.Categories = categories;
            return View(models);
        }
    }
}
