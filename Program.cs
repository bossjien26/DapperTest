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
                Ignore(x => x.User).AfterAction((kind, x) =>
                {
                    if (kind == DapperPlusActionKind.Insert || kind == DapperPlusActionKind.Merge)
                    {
                        x.User.ForEach(user => { user.UserGroupId = x.Id; });
                    }
                });


            var UserGroup = new List<UserGroup>() {
                new UserGroup{
                    Name = "Group1",
                    User = new List<User>{
                        new User{
                            Name = "小明"
                        },
                        new User{
                            Name = "小霞"
                        },
                        new User{
                            Name = "Sum"
                        }
                    }
                },new UserGroup{
                    Name = "Group2",
                    User = new List<User>{
                        
                    }
                }
            };

            _conn.BulkMerge(UserGroup).ThenBulkMerge(x => x.User);


            Console.WriteLine(elapsedMs);
            watch.Stop();
        }

    }
}

