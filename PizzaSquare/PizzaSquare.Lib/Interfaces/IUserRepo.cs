using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaSquare.Lib.Interfaces
{
    public interface IUserRepo
    {
        public IEnumerable<Users> GetUsers();
        public Users GetUserByID(int id);
        public void AddUser(Users user);

        public Users GetCurrUser();
        public void SetCurrUser(Users u);

    }
}
