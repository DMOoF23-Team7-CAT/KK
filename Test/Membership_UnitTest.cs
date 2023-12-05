using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KK.Models.Entities;
using KK.Models.Repositories;
using KK.Models.Interfaces;

namespace Test
{
    [TestClass]
    public class MembershipRepository_UnitTest
    {
        CustomerRepository customerRepo = new CustomerRepository();

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
                CustomerId = 2,                
            };
            newMembership.Customer = customerRepo.GetById(newMembership.CustomerId);

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
                CustomerId = 2,
            };
            newMembership.Customer = customerRepo.GetById(newMembership.CustomerId);

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
                CustomerId = 2,
            };
            newMembership.Customer = customerRepo.GetById(newMembership.CustomerId);

            // Act
            membershipRepository.Add(newMembership);
            newMembership.IsActive = false;
            membershipRepository.Update(newMembership);
            Membership updatedMembership = membershipRepository.GetById(newMembership.Id);

            // Assert
            Assert.IsNotNull(updatedMembership);
            Assert.IsFalse(updatedMembership.IsActive);

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
                CustomerId = 2,
            };
            newMembership.Customer = customerRepo.GetById(newMembership.CustomerId);

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
                CustomerId = 2,
            };
            membership1.Customer = customerRepo.GetById(membership1.CustomerId);

            Membership membership2 = new Membership
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                IsActive = true,
                CustomerId = 3,
            };
            membership2.Customer = customerRepo.GetById(membership2.CustomerId);

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
