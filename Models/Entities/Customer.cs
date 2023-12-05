using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KK.Models.Entities.Enum;

namespace KK.Models.Entities
{
    public class Customer
    {
        // Primary Key
        public int Id { get; set; }

        // Attributes
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool HasSignedDisclaimer { get; set; } = false; // default value

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public Qualification Certification { get; set; } = Qualification.None; // default value

        // Navigation Property
        public Membership? Membership { get; set; }
        public ICollection<Entry>? Entries { get; set; }

        // Constructor
        public Customer()
        {
        }
    }
}
