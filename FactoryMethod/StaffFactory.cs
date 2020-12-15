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
	public abstract class StaffFactory: Controller, IAcademicManagement<Staff>
    {
        public AcademicContext context;

        public StaffFactory(AcademicContext context)
        {
            this.context = context;
        }

        public void AddNew(Staff staff)
        {
            if (staff != null)
            {
                this.context.Add(staff);
                this.context.SaveChanges();
            }
        }

		public void DeleteModel(int id)
        {
            var staff = SearchById(id);
            this.context.Staffs.Remove(staff);
            this.context.SaveChanges();
        }

        public void EditModel(Staff staff)
        {
            if (staff != null)
            {
                this.context.Update(staff);
                this.context.SaveChanges();
            }
        }

		public Staff SearchById(int id)
        {
            var staffs = this.context.Staffs.Find(id);
            return staffs;
        }

        public List<Staff> SearchByName(string name)
        {
            var staffs = this.context.Staffs.ToList().FindAll(staff => staff.Name.Contains(name));
            return staffs;
        }

        public List<Staff> ViewAll()
        {
            var staffs = this.context.Staffs.ToList();
            return staffs;
        }

	}
}
