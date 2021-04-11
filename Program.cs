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
            // DapperPlusManager.Entity<UserGroup>().Table("UserGroup").Identity(x => x.Id).
            // Ignore(x => x.User).AfterAction((kind,x) => {
            //     if(kind == DapperPlusActionKind.Insert || kind == DapperPlusActionKind.Merge){
            //         x.User.UserGroupId = x.Id;
            //     }
            // });

            var user = new List<User>{
                new User{
                    Name = "UserA",
                    UserGroupId = 1
                }
            };

            _conn.BulkMerge(user);

            // var user = new User { Account = "user1" };
            // var userGroup = new UserGroup{Name = "group1",User = user};
            // _conn.BulkInsert(userGroup).ThenBulkInsert(x => x.User);
            Console.WriteLine(elapsedMs);
            watch.Stop();
        }

    }
}

