using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicManagement.Models
{
    public class Course
    {
        public Course()
        {
            Trainees = new HashSet<Trainee>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category{ get; set; }
        public ICollection<Trainee> Trainees { get; set; }
    }
}