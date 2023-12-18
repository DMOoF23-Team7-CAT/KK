using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK.Models.Entities
{
    public class Membership
    {
        // Primary Key
        public int Id { get; set; }

        // Attributes
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        // Foreign Key
        public int CustomerId { get; set; }

        // Navigation Propperty
        public Customer Customer { get; set; }

        // Constructor
        public Membership()
        {
            SetStartDate();
        }
        // Sets startdate to curent time if the date hasnt already been set
        private void SetStartDate()
        {
            if (StartDate == DateTime.MinValue)
            {
                StartDate = DateTime.Now;
            }
        }
    }
}
