using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Song
    {
        public Song()
        {
            Collections = new HashSet<Collection>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ArtistId { get; set; }
        public int Likes { get; set; }
        public int Price { get; set; }
        public int Sales { get; set; }
        public int? Rating { get; set; }

        public virtual Artist Artist { get; set; } = null!;
        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
