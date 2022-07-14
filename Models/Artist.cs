using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Artist
    {
        public Artist()
        {
            Collections = new HashSet<Collection>();
            Songs = new HashSet<Song>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
