using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK.Models.Entities
{
    public class ServiceItem
    {
        // Primary Key
        public int Id { get; set; }

        // Attributes
        public string Name { get; set; }
        public decimal Price => GetItemPrice(Name);

        // Foreign Key
        public int EntryId { get; set; }

        // Method to set Price in acordence with the coresponding name
        private static decimal GetItemPrice(string name)
        {
            return name switch
            {
                "MONTH" => 500.00m,
                "QYARTER" => 1200.00m,
                "YEAR" => 4000.00m,
                "CHILD" => 60.00m,
                "DAY" => 120.00m,
                "TENTIMES" => 1000.00m,

                "HARNESS" => 25.00m,
                "CLIMBINGSHOES" => 40.00m,
                "ROPE" => 50.00m,
                _ => throw new ArgumentOutOfRangeException(nameof(name), name, null),
            };
        }

        // Constructor that takes one parameter  for Name and change the string to uppercase
        public ServiceItem(string name)
        {
            Name = name.ToUpper();
        }
    }
}
