using EnscryptDb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnscryptDb.IRepository
{
    public interface IWacsRepository
    {
        List<User> GetUsers();
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(string userName);
        bool CreateTableUser();
        User GetUserbyUserName(string userName);
    }
}
