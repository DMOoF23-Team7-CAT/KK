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
        public int Id { get; set; }

        // Attributes
        public DateTime CheckInTime { get; set; }
        public decimal Price { get; set; }

        // Foreign Key
        public int CustomerId { get; set; }

        // Navigation Property
        public Customer Customer { get; set; }
        public ICollection<ServiceItem>? Items { get; set; }

        // Constructor that sets Checkintime to the current time and calculate the Price
        public Entry()
        {
            CheckInTime = DateTime.Now;
            CalculateTotalPrice();
        }

        // Method to Add Equipment and make new list if the list is null
        public void AddServiceItem(ServiceItem item)
        {
            if (Items == null)
            {
                Items = new List<ServiceItem>();
            }

            Items.Add(item);
            CalculateTotalPrice();
        }


        // Method to calculate the Price from the two list, if both list are null the Price = 0
        private void CalculateTotalPrice()
        {
            Price = 0;

            if (Items != null)
            {
                foreach (var i in Items)
                {
                    Price += i.Price;
                }
            }


        }
    }   
}
