using System;
using System.Collections.Generic;

namespace PizzaSquare.Lib
{
    public partial class Orders
    {
        public Orders()
        {
            OrderPizzas = new HashSet<OrderPizzas>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? StoreId { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual Stores Store { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<OrderPizzas> OrderPizzas { get; set; }
    }
}
