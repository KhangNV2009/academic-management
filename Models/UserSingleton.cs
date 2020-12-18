using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademicManagement.Models
{
    public class UserSingleton
    {
        private static UserSingleton instance = new UserSingleton();
        private UserSingleton() { }
        public static UserSingleton getIntance()
        {
            return instance;
        }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
