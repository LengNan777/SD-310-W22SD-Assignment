using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
        public DateTime Date { get; set; }

        public virtual Song Song { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
