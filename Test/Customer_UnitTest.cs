using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KK.Models.Entities;
using KK.Models.Repositories;
using KK.Models.Interfaces;
using KK.Models.Entities.Enum;
using System.Collections.ObjectModel;

namespace Test
{
    [TestClass]
    public class CustomerRepository_UnitTest
    {
        [TestMethod]
        public void AddCustomer_Test()
        {
            // Arrange
            ICustomerRepository customerRepository = new CustomerRepository();
            Customer newCustomer = new Customer
            {
                Name = "John Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Qualification = Qualification.TopRope, // Updated property
                HasSignedDisclaimer = true
            };

            // Act
            customerRepository.Add(newCustomer);

            // Assert
            Assert.IsNotNull(newCustomer.Id);
            Assert.AreNotEqual(0, newCustomer.Id);

            customerRepository.Remove(newCustomer);
        }

        [TestMethod]
        public void GetCustomerById_Test()
        {
            // Arrange
            ICustomerRepository customerRepository = new CustomerRepository();
            Customer newCustomer = new Customer
            {
                Name = "John Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Qualification = Qualification.TopRope, // Updated property
                HasSignedDisclaimer = true
            };

            // Act
            customerRepository.Add(newCustomer);
            Customer retrievedCustomer = customerRepository.GetById(newCustomer.Id);

            // Assert
            Assert.IsNotNull(retrievedCustomer);
            Assert.AreEqual(newCustomer.Id, retrievedCustomer.Id);
            Assert.AreEqual(newCustomer.Qualification, retrievedCustomer.Qualification);

            customerRepository.Remove(newCustomer);
        }

        [TestMethod]
        public void UpdateCustomer_Test()
        {
            // Arrange
            ICustomerRepository customerRepository = new CustomerRepository();
            Customer newCustomer = new Customer
            {
                Name = "John Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Qualification = Qualification.TopRope, // Updated property
                HasSignedDisclaimer = true
            };

            // Act
            customerRepository.Add(newCustomer);
            newCustomer.Name = "Updated Name";
            customerRepository.Update(newCustomer);
            Customer updatedCustomer = customerRepository.GetById(newCustomer.Id);

            // Assert
            Assert.IsNotNull(updatedCustomer);
            Assert.AreEqual("Updated Name", updatedCustomer.Name);

            customerRepository.Remove(newCustomer);
        }

        [TestMethod]
        public void RemoveCustomer_Test()
        {
            // Arrange
            ICustomerRepository customerRepository = new CustomerRepository();
            Customer newCustomer = new Customer
            {
                Name = "John Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Qualification = Qualification.TopRope, // Updated property
                HasSignedDisclaimer = true
            };

            // Act
            customerRepository.Add(newCustomer);
            customerRepository.Remove(newCustomer);
            Customer deletedCustomer = customerRepository.GetById(newCustomer.Id);

            // Assert
            Assert.IsNull(deletedCustomer);
        }

        [TestMethod]
        public void GetAllCustomers_Test()
        {
            // Arrange
            ICustomerRepository customerRepository = new CustomerRepository();
            Customer customer1 = new Customer
            {
                Name = "John Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Qualification = Qualification.TopRope, // Updated property
                HasSignedDisclaimer = true
            };

            Customer customer2 = new Customer
            {
                Name = "Jane Doe",
                DateOfBirth = new DateTime(1985, 5, 10),
                Phone = "987-654-3210",
                Email = "jane.doe@example.com",
                Qualification = Qualification.Lead, // Updated property
                HasSignedDisclaimer = false
            };

            // Act
            customerRepository.Add(customer1);
            customerRepository.Add(customer2);
            IEnumerable<Customer> allCustomers = customerRepository.GetAll();

            // Assert
            Assert.IsTrue(allCustomers.Any(c => c.Name == customer1.Name));
            Assert.IsTrue(allCustomers.Any(c => c.Name == customer2.Name));

            customerRepository.Remove(customer1);
            customerRepository.Remove(customer2);
        }

        
        [TestMethod]
        public void GetCustomer_Test()
        {
            // Arrange
            ICustomerRepository customerRepository = new CustomerRepository();
            IMembershipRepository membershipRepository = new MembershipRepository();
            IEntryRepository entryRepository = new EntryRepository();
            IServiceItemRepository serviceItemRepository = new ServiceItemRepository();

            Customer newCustomer = new Customer
            {
                Name = "John Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "123-456-7890",
                Email = "john.doe@example.com",
                Qualification = Qualification.TopRope,
                HasSignedDisclaimer = true
            };
            customerRepository.Add(newCustomer); // add customer to repository have to do it to get the id

            Membership membership = new Membership
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                IsActive = true,
                Customer = newCustomer,
                CustomerId = newCustomer.Id
            };
            membershipRepository.Add(membership); // add membership to repository

            Entry en = new Entry
            {
                Customer = newCustomer,
                CustomerId = newCustomer.Id,
                
            };
            entryRepository.Add(en); // add entry membership to repository

            ServiceItem item = new ServiceItem 
            {
                Name = "month" ,
                EntryId = en.Id
            };
            serviceItemRepository.Add(item); // add item membership to repository

            // Act
            Customer retrievedCustomer = customerRepository.GetCustomer(newCustomer.Id);

            // Assert
            Assert.IsNotNull(retrievedCustomer);
            Assert.AreEqual(newCustomer.Id, retrievedCustomer.Id);

            // Test Customer properties
            Assert.AreEqual("John Doe", retrievedCustomer.Name);
            Assert.AreEqual(new DateTime(1990, 1, 1), retrievedCustomer.DateOfBirth);
            Assert.AreEqual("123-456-7890", retrievedCustomer.Phone);
            Assert.AreEqual("john.doe@example.com", retrievedCustomer.Email);
            Assert.AreEqual(Qualification.TopRope, retrievedCustomer.Qualification);
            Assert.IsTrue(retrievedCustomer.HasSignedDisclaimer);

            // Test Membership properties
            Assert.IsNotNull(retrievedCustomer.Membership);
            Assert.IsNotNull(retrievedCustomer.Membership.Id);
            Assert.IsNotNull(retrievedCustomer.Membership.StartDate);
            Assert.IsNotNull(retrievedCustomer.Membership.EndDate);
            Assert.IsNotNull(retrievedCustomer.Membership.IsActive);

            // Test Entry properties
            Assert.IsNotNull(retrievedCustomer.Entries);
            foreach (var entry in retrievedCustomer.Entries)
            {
                Assert.IsNotNull(entry.Id);
                Assert.IsNotNull(entry.CheckInTime);
                Assert.IsNotNull(entry.Price);

                // Test ServiceItem properties within Entry
                Assert.IsNotNull(entry.Items);
                foreach (var serviceItem in entry.Items)
                {
                    Assert.IsNotNull(serviceItem.Id);
                    Assert.IsNotNull(serviceItem.Name);
                    Assert.IsNotNull(serviceItem.EntryId);
                }
            }

            customerRepository.Remove(newCustomer);
            membershipRepository.Remove(membership);
            entryRepository.Remove(en);
            serviceItemRepository.Remove(item);
        }

        
    }
}
