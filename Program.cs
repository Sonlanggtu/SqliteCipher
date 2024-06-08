using EnscryptDb.Core;
using EnscryptDb.Helper;
using EnscryptDb.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EnscryptDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                var connectionFactory = ConnectionHelper.GetConnection();
                var context = new DbContext(connectionFactory);
                var userRepo = new WacsRepository(context);

                var user = new Model.User() { Id = Guid.NewGuid().ToString(), UserName = $"{Guid.NewGuid()}_user", Password = "password" };
                userRepo.CreateUser(user);

                var users = userRepo.GetUsers();
                var userbyUserName = userRepo.GetUserbyUserName("49fdc3cf-a596-4650-aa60-af89ae42f549_user");

                foreach (var item in users)
                {
                    Console.WriteLine($"Id:{item.Id} - UsernName: {item.UserName} - Passowrd:{item.Password}");
                }

                //string sql = "Data Source=database.db; password =Sondn@123";
                //SQLiteConnection conn = new SQLiteConnection(sql);
                //conn.Open();
                //CreateTable(conn);
                //InsertData(conn, Guid.NewGuid().ToString(), $"{Guid.NewGuid()}_user", "password");
                //InsertData(conn, Guid.NewGuid().ToString(), $"{Guid.NewGuid()}_user", "password");
                //InsertData(conn, Guid.NewGuid().ToString(), $"{Guid.NewGuid()}_user", "password");
                //InsertData(conn, Guid.NewGuid().ToString(), $"{Guid.NewGuid()}_user", "password");
                //InsertData(conn, Guid.NewGuid().ToString(), $"{Guid.NewGuid()}_user", "password");
                //GetAllUser(conn);
                Console.ReadKey();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //private static void GetAllUser(SQLiteConnection conn)
        //{
        //    string sql = @"select Id, UserName, Password from User";
        //    var cmd = new SQLiteCommand(sql, conn);
        //    var reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        string id = reader.GetString(0);
        //        string username = reader.GetString(1);
        //        string password = reader.GetString(2);
        //        Console.WriteLine($"Id:{id} - UsernName: {username} - Passowrd:{password}");
        //    }
        //}

        //private static void InsertData(SQLiteConnection conn, string id, string userName, string password)
        //{
        //    string sql = @"insert into User(Id, UserName, Password)
        //                                values (@id, @userName, @password)";
        //    var cmd = new SQLiteCommand(sql, conn);
        //    cmd.Parameters.AddWithValue("@id", id);
        //    cmd.Parameters.AddWithValue("@userName", userName);
        //    cmd.Parameters.AddWithValue("@password", password);
        //    cmd.ExecuteNonQuery();

        //}

        //private static void CreateTable (SQLiteConnection conn)
        //{
        //    string sql = @"create table if not exists User(Id varchar(100),
        //                                                   UserName varchar(255) PRIMARY KEY,
        //                                                   Password varchar(255))";
        //    var cmd = new SQLiteCommand(sql, conn);
        //    cmd.ExecuteNonQuery();

        //}
    }
}
