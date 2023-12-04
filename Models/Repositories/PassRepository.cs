using System;
using System.Collections.Generic;
using System.Data;
using KK.Models.Entities;
using KK.Models.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KK.Models.Repositories
{
    public class PassRepository : IPassRepository
    {
        private readonly string _connectionString;

        public PassRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Pass entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO kk_PASS (Name, Price, EntryId) VALUES (@Name, @Price, @EntryId); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Price", entity.Price);
                    command.Parameters.AddWithValue("@EntryId", entity.Entry.Id); // Assuming Entry is a navigation property

                    // Execute the SQL command and get the inserted ID
                    entity.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public IEnumerable<Pass> GetAll()
        {
            List<Pass> passes = new List<Pass>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_PASS", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            passes.Add(MapDataToPass(reader));
                        }
                    }
                }
            }

            return passes;
        }

        public Pass GetById(int id)
        {
            Pass pass = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_PASS WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pass = MapDataToPass(reader);
                        }
                    }
                }
            }

            return pass;
        }

        public void Remove(Pass entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM kk_PASS WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Pass entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE kk_PASS SET Name = @Name, Price = @Price, EntryId = @EntryId WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Price", entity.Price);
                    command.Parameters.AddWithValue("@EntryId", entity.Entry.Id); // Assuming Entry is a navigation property

                    command.ExecuteNonQuery();
                }
            }
        }

        private static Pass MapDataToPass(SqlDataReader reader)
        {
            return new Pass
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                EntryId = Convert.ToInt32(reader["EntryId"]),
                Entry = null // Set Entry property if needed
                // Add other mapping as needed
            };
        }
    }
}
