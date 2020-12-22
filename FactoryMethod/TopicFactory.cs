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
	public class TopicFactory : Controller, IAcademicManagement<Topic>
	{
		public AcademicContext context;
		public TopicFactory(AcademicContext context)
		{
			this.context = context;
		}
		public void AddNew(Topic topic)
		{
			if (topic != null)
			{
				this.context.Add(topic);
				this.context.SaveChanges();
			}
		}

		public void DeleteModel(int id)
		{
			var topic = SearchById(id);
			this.context.Topics.Remove(topic);
			this.context.SaveChanges();
		}

		public void EditModel(Topic topic)
		{
			if (topic != null)
			{
				this.context.Update(topic);
				this.context.SaveChanges();
			}
		}

		public Topic SearchById(int id)
		{
			var topics = this.context.Topics.Find(id);
			return topics;
		}

		public List<Topic> SearchByName(string name)
		{
			var topics = this.context.Topics.ToList().FindAll(topic => topic.Name.Contains(name));
			return topics;
		}

		public List<Topic> ViewAll()
		{
			var topics = this.context.Topics.ToList();
			return topics;
		}
	}
}
