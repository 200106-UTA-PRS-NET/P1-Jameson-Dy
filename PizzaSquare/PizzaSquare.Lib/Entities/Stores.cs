using System.Collections.Generic;

namespace PizzaSquare.Lib
{
    public partial class Stores
    {
        public Stores()
        {
            Orders = new HashSet<Orders>();
            StorePizzas = new HashSet<StorePizzas>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<StorePizzas> StorePizzas { get; set; }
    }
}
