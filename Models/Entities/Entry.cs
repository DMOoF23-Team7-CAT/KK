using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK.Models.Entities
{
    public class Entry
    {
        // Primary Key
        public int EntryId { get; set; }

        // Attributes
        public DateTime CheckInTime { get; set; }
        public float Price { get; set; }

        // Foreign Key
        public int CustomerId { get; set; }

        // Navigation Property
        public Customer Customer { get; set; }
        public ICollection<Pass>? Passes { get; set; }
        public ICollection<Equipment>? Equipment { get; set; }
    }   
}
