using System.ComponentModel.DataAnnotations;

namespace PizzaSquare.Web.Models
{
    public class StoreViewModel
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; }
        public int Id { get; set; }
        public string WaitTime { get; set; }
    }
}
