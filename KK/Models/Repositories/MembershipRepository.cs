using System;
using System.Collections.Generic;
using System.Data;
using KK.Models.Entities;
using KK.Models.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KK.Models.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly string _connectionString;

        public MembershipRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Membership entity)
        {
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
        }

        public IEnumerable<Membership> GetAll()
        {
            List<Membership> memberships = new List<Membership>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_MEMBERSHIP", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            memberships.Add(MapDataToMembership(reader));
                        }
                    }
                }
            }

            return memberships;
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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM kk_MEMBERSHIP WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Membership entity)
        {
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
                Customer = null // Set Customer property if needed
                // Add other mapping as needed
            };
        }
    }
}
