using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement.Models;
using AcademicManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicManagement.FactoryMethod
{
    public abstract class TraineesFactory : Controller, IAcademicManagement<Trainee>
    {
        public AcademicContext context;
        
        public TraineesFactory(AcademicContext context)
        {
            this.context = context;
        }

        public void AddNew(Trainee trainee)
        {
            if(trainee != null)
            {
                this.context.Add(trainee);
                this.context.SaveChanges();
            }
        }

        public void DeleteModel(int id)
        {
            var trainee = SearchById(id);
            this.context.Trainees.Remove(trainee);
            this.context.SaveChanges();
        }

        public void EditModel(Trainee trainee)
        {
            if(trainee != null)
            {
                this.context.Update(trainee);
                this.context.SaveChanges();
            }
        }

        public Trainee SearchById(int id)
        {
            var trainee = this.context.Trainees.Find(id);
            return trainee;
        }

        public List<Trainee> SearchByName(string name)
        {
            var trainees = this.context.Trainees.ToList().FindAll(trainee => trainee.Name.Contains(name));
            return trainees;
        }

        public List<Trainee> ViewAll()
        {
            var trainees = this.context.Trainees.ToList();
            return trainees;
        }
    }
}
