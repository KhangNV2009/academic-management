using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement.Models;
using AcademicManagement.Data;
 
namespace AcademicManagement.FactoryMethod
{
    public abstract class CategoriesFactory : Controller, IAcademicManagement<Category>
    {
        public AcademicContext context;

        public CategoriesFactory(AcademicContext context)
        {
            this.context = context;
        }
        public void AddNew(Category category)
        {
          if (category != null)
            {
                this.context.Add(category);
                this.context.SaveChanges();
            }
        }

        public void DeleteModel(int id)
        {
            var category = SearchById(id);
            this.context.Categories.Remove(category);
            this.context.SaveChanges();
        }

        public void EditModel(Category category)
        {
            if (category != null)
            {
                this.context.Update(category);
                this.context.SaveChanges();
            }
        }

        public Category SearchById(int id)
        {
            var category = this.context.Categories.Find(id);
            return category;
        }

        public List<Category> SearchByName(string name)
        {
            var category = this.context.Categories.ToList().FindAll(category => category.Name.Contains(name));
            return category;
        }

        public List<Category> ViewAll()
        {
            var category = this.context.Categories.ToList();
            return category;
        }
    }
}
