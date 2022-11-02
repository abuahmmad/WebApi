using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Models
{
    public class UserRole
    {
        public string RoleName { get; set; }

        public string UserId { get; set; }

        public string RoleId { get; set; }

        public bool IsSelected { get; set; }
    }
}
