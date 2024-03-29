﻿using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Artist
    {
        public Artist()
        {
            Songs = new HashSet<Song>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Song> Songs { get; set; }
    }
}
