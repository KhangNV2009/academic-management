using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicManagement.Models
{
    public class TraineeCourse
    {
        public int TraineeId { get; set; }
        public int CourseId { get; set; }
        public Trainee Trainee { get; set; }
        public Course Course { get; set; }
    }
}
