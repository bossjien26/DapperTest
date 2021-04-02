using System;
using System.Collections.Generic;
using Models;
using MySqlConnector;
using Z.Dapper.Plus;

namespace termainalc
{

    class Program
    {
        public static MySqlConnection _conn = new MySqlConnection("server=127.0.0.1;database=mydatabase;user=root;password=Passwo!rd123!");
        static void Main(string[] args)
        {
            using var conn = _conn;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Start();
            var elapsedMs = watch.ElapsedMilliseconds;

            DapperPlusManager.Entity<User>().Table("User").Identity(x => x.ID);
            
            DapperPlusManager.Entity<UserGroup>().Table("UserGroup").Identity(x => x.Id).
            Ignore(x => x.User).AfterAction((kind,x) => {
                if(kind == DapperPlusActionKind.Update || kind == DapperPlusActionKind.Merge){
                    x.User.ForEach(user => user.UserGroupId = x.Id);
                }
            });

            var user = new List<User>{
                new User {ID = 1283, Account = "user1" },
                new User {ID = 1282, Account = "user2" },
                new User {ID = 1281, Account = "user3" }
            };
            var userGroup = new UserGroup{Id = 8,Name = "group2",User = user};
            _conn.BulkUpdate(userGroup).ThenBulkUpdate(x => x.User);
            Console.WriteLine(elapsedMs);
            watch.Stop();
        }
    }
}

