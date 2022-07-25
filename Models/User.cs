using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class User
    {
        public User()
        {
            Collections = new HashSet<Collection>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Balance { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
