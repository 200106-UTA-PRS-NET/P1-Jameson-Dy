using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSquare.Lib.Interfaces
{
    public interface IUserRepo
    {
        public IEnumerable<Users> GetUsers();
        public Users GetUserByID(int id);
    }
}
