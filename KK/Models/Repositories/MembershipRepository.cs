using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using KK.Models.Entities;
using KK.Models.Entities.Enum;
using KK.Models.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KK.Models.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly string _connectionString;
        public ObservableCollection<Membership> Memberships = new ObservableCollection<Membership>();

        public MembershipRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Membership entity)
        {
            // Adds the entity to the Database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO kk_MEMBERSHIP (StartDate, EndDate, IsActive, CustomerId) VALUES (@StartDate, @EndDate, @IsActive, @CustomerId); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@StartDate", entity.StartDate);
                    command.Parameters.AddWithValue("@EndDate", entity.EndDate);
                    command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                    command.Parameters.AddWithValue("@CustomerId", entity.Customer.Id); // Assuming Customer is a navigation property

                    // Execute the SQL command and get the inserted ID
                    entity.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            // Adds the entity to the ObservableCollection
            if (!Memberships.Contains(entity)) { Memberships.Add(entity); };
        }

        public IEnumerable<Membership> GetAll()
        {
            Memberships = new ObservableCollection<Membership>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_MEMBERSHIP", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Memberships.Add(MapDataToMembership(reader));
                        }
                    }
                }
            }

            return Memberships;
        }

        public Membership GetById(int id)
        {
            Membership membership = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_MEMBERSHIP WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            membership = MapDataToMembership(reader);
                        }
                    }
                }
            }

            return membership;
        }

        public void Remove(Membership entity)
        {
            // Removes entity from Database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM kk_MEMBERSHIP WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.ExecuteNonQuery();
                }
            }
            // Removes entity from Collection
            if (Memberships.Contains(entity)) { Memberships.Remove(entity); };
        }

        public void Update(Membership entity)
        {
            // Updates Database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE kk_MEMBERSHIP SET StartDate = @StartDate, EndDate = @EndDate, IsActive = @IsActive, CustomerId = @CustomerId WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@StartDate", entity.StartDate);
                    command.Parameters.AddWithValue("@EndDate", entity.EndDate);
                    command.Parameters.AddWithValue("@IsActive", entity.IsActive);
                    command.Parameters.AddWithValue("@CustomerId", entity.Customer.Id); // Assuming Customer is a navigation property

                    command.ExecuteNonQuery();
                }
            }
            // Adds entity To Collection or Updates it
            if (!Memberships.Contains(entity))
            {
                Memberships.Add(entity);
            }
            else
            {
                var existingEntity = Memberships.FirstOrDefault(e => e.Id == entity.Id);
                if (existingEntity != null)
                {
                    int index = Memberships.IndexOf(existingEntity);
                    Memberships[index] = entity;
                }
            }
        }

        private static Membership MapDataToMembership(SqlDataReader reader)
        {
            return new Membership
            {
                Id = Convert.ToInt32(reader["Id"]),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                EndDate = Convert.ToDateTime(reader["EndDate"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CustomerId = Convert.ToInt32(reader["CustomerId"]),
            };
        }
    }
}
