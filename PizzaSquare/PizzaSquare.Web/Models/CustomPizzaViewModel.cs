﻿using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
