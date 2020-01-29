using PizzaSquare.Lib;
using PizzaSquare.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaSquare.Data.Repositories
{
    public class StoreRepo : IStoreRepo
    {
        PizzaSquareContext db;

        public StoreRepo()
        {
            db = new PizzaSquareContext();
        }
        public StoreRepo(PizzaSquareContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Stores> GetStores()
        {
            var query = db.Stores.Select(s => s);
            return query;
        }
    }
}
