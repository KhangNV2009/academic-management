using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement.Data;
using AcademicManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagement.FactoryMethod
{
    public class TrainersFactory : Controller, IAcademicManagement<Trainer>
    {
        public AcademicContext context;

        public TrainersFactory(AcademicContext context)
        {
            this.context = context;
        }
        public void AddNew(Trainer trainer)
        {
            if (trainer != null)
            {
                this.context.Add(trainer);
                this.context.SaveChanges();
            }
        }

        public void DeleteModel(int id)
        {
            var trainer = SearchById(id);
            this.context.Trainers.Remove(trainer);
            this.context.SaveChanges();
        }

        public void EditModel(Trainer trainer)
        {
            if(trainer != null)
            {
                this.context.Update(trainer);
                this.context.SaveChanges();
            }
        }

        public Trainer SearchById(int id)
        {
            var trainer = this.context.Trainers.Find(id);
            return trainer;
        }

        public List<Trainer> SearchByName(string name)
        {
            var trainers = this.context.Trainers.ToList().FindAll(trainer => trainer.Name.Contains(name));
            return trainers;
        }

        public List<Trainer> ViewAll()
        {
            var trainers = this.context.Trainers.ToList();
            return trainers;
        }
    }
}
