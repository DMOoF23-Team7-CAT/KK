using System;
using System.Collections.Generic;
using System.Data;
using KK.Models.Entities;
using KK.Models.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KK.Models.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Customer entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO kk_CUSTOMER (Name, DateOfBirth, Phone, Email, Qualifications, HasSignedDisclaimer) VALUES (@Name, @DateOfBirth, @Phone, @Email, @Qualifications, @HasSignedDisclaimer); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
                    command.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", entity.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Qualifications", entity.Qualifications ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@HasSignedDisclaimer", entity.HasSignedDisclaimer);

                    // Execute the SQL command and get the inserted ID
                    entity.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_CUSTOMER", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(MapDataToCustomer(reader));
                        }
                    }
                }
            }

            return customers;
        }

        public Customer GetById(int id)
        {
            Customer customer = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_CUSTOMER WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = MapDataToCustomer(reader);
                        }
                    }
                }
            }

            return customer;
        }

        public void Remove(Customer entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM kk_CUSTOMER WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Customer entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE kk_CUSTOMER SET Name = @Name, DateOfBirth = @DateOfBirth, Phone = @Phone, Email = @Email, Qualifications = @Qualifications, HasSignedDisclaimer = @HasSignedDisclaimer WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
                    command.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", entity.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Qualifications", entity.Qualifications ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@HasSignedDisclaimer", entity.HasSignedDisclaimer);

                    command.ExecuteNonQuery();
                }
            }
        }

        private static Customer MapDataToCustomer(SqlDataReader reader)
        {
            return new Customer
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                Phone = reader["Phone"] is DBNull ? null : reader["Phone"].ToString(),
                Email = reader["Email"] is DBNull ? null : reader["Email"].ToString(),
                Qualifications = reader["Qualifications"] is DBNull ? null : reader["Qualifications"].ToString(),
                HasSignedDisclaimer = Convert.ToBoolean(reader["HasSignedDisclaimer"])
            };
        }
    }
}
