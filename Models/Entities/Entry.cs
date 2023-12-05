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
        public ICollection<Pass>? Passes { get; set; }
        public ICollection<Equipment>? Equipment { get; set; }

        // Constructor
        public Entry()
        {
            CheckInTime = DateTime.Now;
            CalculateTotalPrice();
        }


        // Method to Add Equipment and make new list if the list is null
        public void AddEquipment(Equipment equipment)
        {
            if (Equipment == null)
            {
                Equipment = new List<Equipment>();
            }

            Equipment.Add(equipment);
            CalculateTotalPrice();
        }

        // Method to Add Pass and make new list if the list is null
        public void AddPass(Pass pass)
        {
            if (Passes == null)
            {
                Passes = new List<Pass>();
            }

            Passes.Add(pass);
            CalculateTotalPrice();
        }

        // Method to calculate the Price from the two list, if both list are null the Price = 0
        private void CalculateTotalPrice()
        {
            Price = 0;

            if (Equipment != null)
            {
                foreach (var equipment in Equipment)
                {
                    Price += equipment.Price;
                }
            }

            if (Passes != null)
            {
                foreach (var pass in Passes)
                {
                    Price += pass.Price;
                }
            }


        }
    }   
}
