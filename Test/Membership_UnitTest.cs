using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KK.Models.Entities;
using KK.Models.Repositories;
using KK.Models.Interfaces;
using System.Collections.ObjectModel;

namespace Test
{
    [TestClass]
    public class MembershipRepository_UnitTest
    {
        ObservableCollection<Customer> Customers = new ObservableCollection<Customer>();

        [TestInitialize]
        public void TestInitialize()
        {
            CustomerRepository customerRepo = new CustomerRepository();
            customerRepo.GetAll();
            Customers = customerRepo.Customers;
        }

        [TestMethod]
        public void AddMembership_Test()
        {
            // Arrange
            IMembershipRepository membershipRepository = new MembershipRepository();
            Membership newMembership = new Membership
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                IsActive = true,
                CustomerId = Customers.FirstOrDefault().Id,
                Customer = Customers.FirstOrDefault()
            };

            // Act
            membershipRepository.Add(newMembership);

            // Assert
            Assert.IsNotNull(newMembership.Id);
            Assert.AreNotEqual(0, newMembership.Id);

            membershipRepository.Remove(newMembership);
        }

        [TestMethod]
        public void GetMembershipById_Test()
        {
            // Arrange
            IMembershipRepository membershipRepository = new MembershipRepository();
            Membership newMembership = new Membership
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                IsActive = true,
                CustomerId = Customers.FirstOrDefault().Id,
                Customer = Customers.FirstOrDefault()
            };

            // Act
            membershipRepository.Add(newMembership);
            Membership retrievedMembership = membershipRepository.GetById(newMembership.Id);

            // Assert
            Assert.IsNotNull(retrievedMembership);
            Assert.AreEqual(newMembership.Id, retrievedMembership.Id);

            membershipRepository.Remove(newMembership);
        }

        [TestMethod]
        public void UpdateMembership_Test()
        {
            // Arrange
            IMembershipRepository membershipRepository = new MembershipRepository();
            Membership newMembership = new Membership
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                IsActive = true,
                CustomerId = Customers.FirstOrDefault().Id,
                Customer = Customers.FirstOrDefault()
            };

            // Act
            membershipRepository.Add(newMembership);
            newMembership.StartDate = new DateTime(2020, 11, 11);
            membershipRepository.Update(newMembership);
            Membership updatedMembership = membershipRepository.GetById(newMembership.Id);

            // Assert
            Assert.IsNotNull(updatedMembership);
            Assert.AreEqual(new DateTime(2020, 11, 11), updatedMembership.StartDate);

            membershipRepository.Remove(updatedMembership);
        }

        [TestMethod]
        public void RemoveMembership_Test()
        {
            // Arrange
            IMembershipRepository membershipRepository = new MembershipRepository();
            Membership newMembership = new Membership
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                IsActive = true,
                CustomerId = Customers.FirstOrDefault().Id,
                Customer = Customers.FirstOrDefault()
            };

            // Act
            membershipRepository.Add(newMembership);
            membershipRepository.Remove(newMembership);
            Membership deletedMembership = membershipRepository.GetById(newMembership.Id);

            // Assert
            Assert.IsNull(deletedMembership);
        }

        [TestMethod]
        public void GetAllMemberships_Test()
        {
            // Arrange
            IMembershipRepository membershipRepository = new MembershipRepository();
            Membership membership1 = new Membership
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                IsActive = true,
                CustomerId = Customers[0].Id,
                Customer = Customers[0]
            };

            Membership membership2 = new Membership
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                IsActive = true,
                CustomerId = Customers[1].Id,
                Customer = Customers[1]
            };

            // Act
            membershipRepository.Add(membership1);
            membershipRepository.Add(membership2);
            IEnumerable<Membership> allMemberships = membershipRepository.GetAll();

            // Assert
            Assert.IsTrue(allMemberships.Any(m => m.CustomerId == membership1.CustomerId));
            Assert.IsTrue(allMemberships.Any(m => m.CustomerId == membership2.CustomerId));

            membershipRepository.Remove(membership1);
            membershipRepository.Remove(membership2);
        }
    }
}
