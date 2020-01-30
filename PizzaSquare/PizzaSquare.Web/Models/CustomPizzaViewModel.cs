using PizzaSquare.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaSquare.Web.Models
{
    public class CustomPizzaViewModel
    {
        [Required]
        public int SelCrustId { get; set; }
        [Required]
        public int SelSauceId { get; set; }
        [Required]
        public int SelCheeseId { get; set; }
        [Required]
        public int SelSizeId { get; set; }
        [Required]
        public int SelTopping1Id { get; set; }
        [Required]
        public int SelTopping2Id { get; set; }
    }
}
