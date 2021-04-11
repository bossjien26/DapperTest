using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> User { get; set; }

    }
}