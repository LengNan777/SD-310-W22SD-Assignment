using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class User
    {
        public User()
        {
            Collections = new HashSet<Collection>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Collection> Collections { get; set; }
    }
}
