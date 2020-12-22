using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicManagement.Models
{
    public class Trainee: Person
    {
        //public Trainee()
        //{
        //    Courses = new HashSet<Course>();
        //}
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Education { get; set; }
        public string MainProgrammingLanguage { get; set; }
        public int TOEICScore { get; set; }
        public string ExperienceDetails { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        //public ICollection<Course> Courses { get; set; }
        public ICollection<TraineeCourse> TraineeCourses { get; set; }
    }
}
