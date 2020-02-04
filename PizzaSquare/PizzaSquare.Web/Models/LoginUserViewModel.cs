using System.ComponentModel.DataAnnotations;

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
