using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicManagement.Models
{
    public class User
    {
        public User(object currentUser)
        {
            if (currentUser is Admin)
            {
                Role = new Admin().GetType().Name;
            }
            if (currentUser is Staff)
            {
                Role = new Admin().GetType().Name;
            }
            if (currentUser is Trainer)
            {
                Role = new Trainer().GetType().Name;
            }
        }
        public string Role { get; set; }
    }
}
