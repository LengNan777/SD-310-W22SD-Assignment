using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Collection
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public int SongId { get; set; }

        public virtual Artist Artist { get; set; } = null!;
        public virtual Song Song { get; set; } = null!;
    }
}
