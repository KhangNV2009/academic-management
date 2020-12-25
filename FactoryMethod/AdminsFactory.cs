using AcademicManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagement.FactoryMethod
{
    public abstract class AdminsFactory: Controller, IAcademicManagement<Admin>
    {
        public AcademicContext context;

        public AdminsFactory(AcademicContext context)
        {
            this.context = context;
        }

        public void AddNew(Admin Admin)
        {
            if (Admin != null)
            {
                this.context.Add(Admin);
                this.context.SaveChanges();
            }
        }

        public void DeleteModel(int id)
        {
            var Admin = SearchById(id);
            this.context.Admins.Remove(Admin);
            this.context.SaveChanges();
        }

        public void EditModel(Admin Admin)
        {
            if (Admin != null)
            {
                this.context.Update(Admin);
                this.context.SaveChanges();
            }
        }

        public Admin SearchById(int id)
        {
            var Admins = this.context.Admins.Find(id);
            return Admins;
        }

        public List<Admin> SearchByName(string name)
        {
            var Admins = this.context.Admins.ToList().FindAll(Admin => Admin.Name.Contains(name));
            return Admins;
        }

        public List<Admin> ViewAll()
        {
            var Admins = this.context.Admins.ToList();
            return Admins;
        }
    }
}
