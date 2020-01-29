﻿using System;
using System.Collections.Generic;

namespace PizzaSquare.Lib
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
