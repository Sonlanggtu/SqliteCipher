using EnscryptDb.Core;
using EnscryptDb.Extensions;
using EnscryptDb.Helper;
using EnscryptDb.IRepository;
using EnscryptDb.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnscryptDb.Repository
{
    public class WacsRepository : RepositoryBase, IWacsRepository
    {
        private DbContext _context;
        public WacsRepository(DbContext context)
            : base(context)
        {
            _context = context;
            CreateTableUser();
        }

        public List<User> GetUsers()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "select Id, UserName, Password from User";
                MapList<User> users = new MapList<User>();
                var res = users.MapToList(command);
                return res;
            }
        }

        public bool CreateUser(User user)
        {

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"insert into User(Id, UserName, Password)
                                        values (@pId, @pUserName, @pPassword)";
                command.Parameters.Add(command.CreateParameter("@pId", user.Id));
                command.Parameters.Add(command.CreateParameter("@pUserName", user.UserName));
                command.Parameters.Add(command.CreateParameter("@pPassword", user.Password));
                return command.ExecuteNonQuery() > 0 ? true : false;
            }
        }

        public bool UpdateUser(User user)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"update User
                                        set Password = @pPassword
                                        where UserName = @pUserName";
                command.Parameters.Add(command.CreateParameter("@pUserName", user.UserName));
                command.Parameters.Add(command.CreateParameter("@pPassword", user.Password));
                return command.ExecuteNonQuery() > 0 ? true : false;
            }
        }

        public bool DeleteUser(string userName)
        {

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"delete User 
                                        where UserName = @pUserName";
                command.Parameters.Add(command.CreateParameter("@pUserName", userName));
                return command.ExecuteNonQuery() > 0 ? true : false;
            }
        }


        public bool CreateTableUser()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"create table if not exists User(Id varchar(100),
                                                           UserName varchar(255) PRIMARY KEY,
                                                           Password varchar(255))";
                return command.ExecuteNonQuery() > 0 ? true : false;
            }
        }

        public User GetUserbyUserName(string userName)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"select Id, UserName, Password from User
                                               where UserName = @pUserName";
                command.Parameters.Add(command.CreateParameter("@pUserName", userName));

                MapList<User> users = new MapList<User>();
                var res = users.MapToList(command).FirstOrDefault();
                return res;
            }
        }
    }
}
