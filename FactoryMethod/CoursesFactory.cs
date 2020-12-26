using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicManagement.FactoryMethod;
using AcademicManagement.Models;
using AcademicManagement.Data;

namespace AcademicManagement.FactoryMethod
{
    public class CoursesFactory : Controller, IAcademicManagement<Course>
    {
        public AcademicContext context;
        public CoursesFactory(AcademicContext context)
        {
            this.context = context;
        }
        public void AddNew(Course course)
        {
            context.Add(course);
            context.SaveChanges();
        }

        public void DeleteModel(int id)
        {
            var course = SearchById(id);
            DeleteTraineeCourses(id);
            context.Courses.Remove(course);
            context.SaveChanges();
        }

        public void EditModel(Course course)
        {
            context.Courses.Update(course);
            context.SaveChanges();
        }

        public Course SearchById(int id)
        {
            var courseTrainees = this.context.CourseTrainees.ToList().FindAll(item => item.CourseId == id);
            var topics = context.Topics.ToList();
            var trainees = this.context.Trainees.ToList();
            var trainers = this.context.Trainers.ToList();
            var course = context.Courses.ToList().Find(item => item.Id == id);
            var category = context.Categories.ToList().Find(item => item.Id == course.Category.Id);

            if (course.Topics != null)
            {
                course.Topics.ToList().ForEach(item =>
                {
                    item = topics.Find(topic => topic.Id == item.Id);
                    item.Trainer = trainers.Find(trainer => trainer.Id == item.Trainer.Id);
                });
            }

            courseTrainees.ForEach(item =>
            {
                var traineeInCourse = trainees.Find(trainee => trainee.Id == item.TraineeId);
                item.Trainee = traineeInCourse;
            });
            course.TraineeCourses = courseTrainees;
            course.Category = category;
            return course;  
        }

        public List<Course> SearchByName(string name)
        {
            return ViewAll().FindAll(item => item.Name.Contains(name));
        }

        public List<Course> ViewAll()
        {
            var courses = this.context.Courses.ToList();
            courses.ForEach(course =>
            {
                course = SearchById(course.Id);
            });
            return courses;
        }
        public void AddNewTraineeCourse(int[] listId, Course course)
        {
            var trainees = this.context.Trainees.ToList();
            foreach (var id in listId)
            {
                TraineeCourse traineeCourse = new TraineeCourse();
                traineeCourse.Course = course;
                traineeCourse.Trainee = trainees.Find(item => item.Id == id);
                context.Add(traineeCourse);
            }
        }
        public void DeleteTraineeCourses(int courseId)
        {
            var course = context.Courses.ToList().Find(item => item.Id == courseId);
            var currentTraineeCourses = this.context.CourseTrainees.ToList().FindAll(item => item.CourseId == courseId);

            if (currentTraineeCourses != null)
            {
                currentTraineeCourses.ForEach(item =>
                {
                    this.context.CourseTrainees.Remove(item);
                    course.TraineeCourses.Remove(item);
                });
            }
        }
    }
}
