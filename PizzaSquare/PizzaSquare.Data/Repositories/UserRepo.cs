using System;
using System.Collections.Generic;
using PizzaSquare.Lib;
using PizzaSquare.Lib.Interfaces;
using System.Linq;

namespace PizzaSquare.Data.Repositories
{
    public class UserRepo : IUserRepo
    {
        readonly PizzaSquareContext db;
        static Users currUser;

        public UserRepo()
        {
            db = new PizzaSquareContext();
        }
        public UserRepo(PizzaSquareContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public Users GetCurrUser()
        {
            return currUser;
        }

        public void SetCurrUser(Users u)
        {
            currUser = u;
        }

        public void AddUser(Users user)
        {
            if (db.Users.Any(u => u.Username == user.Username) || user.Username == null)
            {
                Console.WriteLine($"Username {user.Username} already exists");
                return;
            }
            else
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
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

        public bool Login(Users user)
        {
            try
            {
                var query = db.Users.Where(u => u.Username == user.Username && u.Password == user.Password).Single();
                currUser = query;
                return true;
            } catch (InvalidOperationException)
            {
                return false;
            }
        }

        public void Logout()
        {
            currUser = null;
        }
    }
}
