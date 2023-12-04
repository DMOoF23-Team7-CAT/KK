// EntryRepository.cs
using System;
using System.Collections.Generic;
using System.Data;
using KK.Models.Entities;
using KK.Models.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KK.Models.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly string _connectionString;

        public EntryRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Entry entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO kk_ENTRY (CheckInTime, Price, CustomerId) VALUES (@CheckInTime, @Price, @CustomerId); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@CheckInTime", entity.CheckInTime);
                    command.Parameters.AddWithValue("@Price", entity.Price);
                    command.Parameters.AddWithValue("@CustomerId", entity.Customer.Id); // Assuming Customer is already associated with Entry

                    // Execute the SQL command and get the inserted ID
                    entity.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public IEnumerable<Entry> GetAll()
        {
            List<Entry> entries = new List<Entry>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_ENTRY", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entries.Add(MapDataToEntry(reader));
                        }
                    }
                }
            }

            return entries;
        }

        public Entry GetById(int id)
        {
            Entry entry = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_ENTRY WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            entry = MapDataToEntry(reader);
                        }
                    }
                }
            }

            return entry;
        }

        public void Remove(Entry entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM kk_ENTRY WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Entry entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE kk_ENTRY SET CheckInTime = @CheckInTime, Price = @Price, CustomerId = @CustomerId WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@CheckInTime", entity.CheckInTime);
                    command.Parameters.AddWithValue("@Price", entity.Price);
                    command.Parameters.AddWithValue("@CustomerId", entity.Customer.Id); // Assuming Customer is already associated with Entry

                    command.ExecuteNonQuery();
                }
            }
        }

        private static Entry MapDataToEntry(SqlDataReader reader)
        {
            return new Entry
            {
                Id = Convert.ToInt32(reader["Id"]),
                CheckInTime = Convert.ToDateTime(reader["CheckInTime"]),
                Price = (decimal)reader["Price"],
                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                Customer = null // Set Customer property if needed
                                // Add other mapping as needed
            };
        }

    }
}
