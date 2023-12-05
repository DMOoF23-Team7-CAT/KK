using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KK.Models.Entities;
using KK.Models.Repositories;
using KK.Models.Interfaces;

namespace Test
{
    [TestClass]
    public class ServiceItemRepository_UnitTest
    {
        [TestMethod]
        public void AddServiceItem_Test()
        {
            // Arrange
            IServiceItemRepository serviceItemRepository = new ServiceItemRepository();
            ServiceItem newServiceItem = new ServiceItem
            {
                Name = "MONTH",
                EntryId = 1 // Assuming Entry ID exists in your database
            };

            // Act
            serviceItemRepository.Add(newServiceItem);

            // Assert
            Assert.IsNotNull(newServiceItem.Id);
            Assert.AreNotEqual(0, newServiceItem.Id);

            // Cleanup
            serviceItemRepository.Remove(newServiceItem);
        }

        [TestMethod]
        public void GetServiceItemById_Test()
        {
            // Arrange
            IServiceItemRepository serviceItemRepository = new ServiceItemRepository();
            ServiceItem newServiceItem = new ServiceItem
            {
                Name = "MONTH",
                EntryId = 1 // Assuming Entry ID exists in your database
            };

            // Act
            serviceItemRepository.Add(newServiceItem);
            ServiceItem retrievedServiceItem = serviceItemRepository.GetById(newServiceItem.Id);

            // Assert
            Assert.IsNotNull(retrievedServiceItem);
            Assert.AreEqual(newServiceItem.Id, retrievedServiceItem.Id);

            // Cleanup
            serviceItemRepository.Remove(newServiceItem);
        }

        [TestMethod]
        public void UpdateServiceItem_Test()
        {
            // Arrange
            IServiceItemRepository serviceItemRepository = new ServiceItemRepository();
            ServiceItem newServiceItem = new ServiceItem
            {
                Name = "MONTH",
                EntryId = 1 // Assuming Entry ID exists in your database
            };

            // Act
            serviceItemRepository.Add(newServiceItem);
            newServiceItem.Name = "QUARTER";
            serviceItemRepository.Update(newServiceItem);
            ServiceItem updatedServiceItem = serviceItemRepository.GetById(newServiceItem.Id);

            // Assert
            Assert.IsNotNull(updatedServiceItem);
            Assert.AreEqual("QUARTER", updatedServiceItem.Name);

            // Cleanup
            serviceItemRepository.Remove(updatedServiceItem);
        }

        [TestMethod]
        public void RemoveServiceItem_Test()
        {
            // Arrange
            IServiceItemRepository serviceItemRepository = new ServiceItemRepository();
            ServiceItem newServiceItem = new ServiceItem
            {
                Name = "MONTH",
                EntryId = 1 // Assuming Entry ID exists in your database
            };

            // Act
            serviceItemRepository.Add(newServiceItem);
            serviceItemRepository.Remove(newServiceItem);
            ServiceItem deletedServiceItem = serviceItemRepository.GetById(newServiceItem.Id);

            // Assert
            Assert.IsNull(deletedServiceItem);
        }

        [TestMethod]
        public void GetAllServiceItems_Test()
        {
            // Arrange
            IServiceItemRepository serviceItemRepository = new ServiceItemRepository();
            ServiceItem serviceItem1 = new ServiceItem
            {
                Name = "MONTH",
                EntryId = 1 // Assuming Entry ID exists in your database
            };

            ServiceItem serviceItem2 = new ServiceItem
            {
                Name = "QUARTER",
                EntryId = 2 // Assuming Entry ID exists in your database
            };

            // Act
            serviceItemRepository.Add(serviceItem1);
            serviceItemRepository.Add(serviceItem2);
            IEnumerable<ServiceItem> allServiceItems = serviceItemRepository.GetAll();

            // Assert
            Assert.IsTrue(allServiceItems.Any(si => si.Name == serviceItem1.Name));
            Assert.IsTrue(allServiceItems.Any(si => si.Name == serviceItem2.Name));

            // Cleanup
            serviceItemRepository.Remove(serviceItem1);
            serviceItemRepository.Remove(serviceItem2);
        }
    }
}
