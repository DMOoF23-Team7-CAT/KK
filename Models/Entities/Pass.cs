using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK.Models.Entities
{
    public class Pass
    {
        // Primary Key
        public int Id { get; set; }

        // Attributes
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Foreign Key
        public int EntryId { get; set; }

        // Navigation Property
        public Entry Entry { get; set; }


        // Constructor
        public Pass()
        {
            
        }
    }
}
