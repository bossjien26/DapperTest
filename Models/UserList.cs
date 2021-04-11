using System;

namespace Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name{get;set;}
        public string Account { get; set; }
        public string Password { get; set; }
        public long Mail { get; set; }
        public long Platform { get; set; }
        public DateTime RegisterDate { get; set; }
        public int UserGroupId { get; set; }
    }

}