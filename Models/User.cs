using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicManagement.Models
{
    public class User
    {
        public User(object CurrentUser)
        {
            Role = CurrentUser.GetType().Name;
        }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
