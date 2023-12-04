using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK.Models.Entities
{
    public class Customer
    {
        // Primary Key
        public int CustomerId { get; set; }

        // Attributes
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Qualifications { get; set; }
        public bool? HasSignedDisclaimer { get; set; }

        // Navigation Property
        public Membership? Membership { get; set; }
        public ICollection<Entry>? Entries { get; set; }
    }
}
