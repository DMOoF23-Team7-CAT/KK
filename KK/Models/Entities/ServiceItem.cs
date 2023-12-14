using System;

namespace KK.Models.Entities
{
    public class ServiceItem
    {
        public decimal monthPrice = 500;

        // Private backing field for Name
        private string _name;

        // Primary Key
        public int Id { get; set; }

        // Public getter and setter for Name Ensure the value is always stored in uppercase
        public string Name
        {
            get => _name;
            set => _name = value.ToUpper();
        }

        // Public getter for Price
        public decimal Price => GetItemPrice(Name); // sets default price to the calculated price

        // Foreign Key
        public int EntryId { get; set; }



        // Method to set Price in accordance with the corresponding name
        private static decimal GetItemPrice(string name)
        {
            return name switch
            {
                "MONTH" => 500.00m,
                "QUARTER" => 1200.00m,
                "YEAR" => 4000.00m,
                "CHILD" => 60.00m,
                "DAY" => 120.00m,
                "TENTIMES" => 1000.00m,
                "HARNESS" => 25.00m,
                "SHOES" => 40.00m,
                "ROPE" => 50.00m,
                _ => throw new ArgumentOutOfRangeException(nameof(name), name, null),
            };
        }

        // Constructor
        public ServiceItem()
        {
        }

        public ServiceItem(string name, int entryId)
        {
            Name = name;
            EntryId = entryId;

        }
    }
}
