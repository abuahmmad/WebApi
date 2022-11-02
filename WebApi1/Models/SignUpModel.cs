using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi1.Models
{
    public class SignUpModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Bdate { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }


        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
