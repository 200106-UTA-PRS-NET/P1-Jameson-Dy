using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaSquare.Web.Models
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Username cannot be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
    }
}
