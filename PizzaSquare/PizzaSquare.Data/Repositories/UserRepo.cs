using System;
using System.Collections.Generic;
using System.Text;
using PizzaSquare.Lib;
using PizzaSquare.Lib.Interfaces;
using System.Linq;

namespace PizzaSquare.Data.Repositories
{
    public class UserRepo : IUserRepo
    {
        PizzaSquareContext db;

        public UserRepo()
        {
            db = new PizzaSquareContext();
        }
        public UserRepo(PizzaSquareContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public Users GetUserByID(int id)
        {
            var query = db.Users.Where(u => u.Id == id).Single();

            return query;
        }

        public IEnumerable<Users> GetUsers()
        {
            var query = db.Users.Select(u => u);
            return query;
        }
    }
}
