using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using KK.Models.Entities;
using KK.Models.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KK.Models.Repositories
{
    public class EntryRepository : IEntryRepository
    {
        private readonly string _connectionString;
        public ObservableCollection<Entry> Entries = new ObservableCollection<Entry>();
        public EntryRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Entry entity)
        {
            // Adds entity to database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO kk_ENTRY (CheckInTime, Price, CustomerId) VALUES (@CheckInTime, @Price, @CustomerId); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@CheckInTime", entity.CheckInTime);
                    command.Parameters.AddWithValue("@Price", entity.Price);
                    command.Parameters.AddWithValue("@CustomerId", entity.CustomerId); // Assuming CustomerId is now used

                    // Execute the SQL command and get the inserted ID
                    entity.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            // Adds entity to Collection
            if(!Entries.Contains(entity)) { Entries.Add(entity); }
        }

        public IEnumerable<Entry> GetAll()
        {
            Entries = new ObservableCollection<Entry>();

            // Get all entities from database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_ENTRY", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Entries.Add(MapDataToEntry(reader));
                        }
                    }
                }
            }
            return Entries;
        }

        public Entry GetById(int id)
        {
            Entry entry = null;

            // Get entity from database
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
            // Removes entity from databse
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM kk_ENTRY WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.ExecuteNonQuery();
                }
            }
            // Removes entity from Collection
            if (Entries.Contains(entity)) { Entries.Remove(entity); }
        }

        public void Update(Entry entity)
        {
            // Updates database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("UPDATE kk_ENTRY SET CheckInTime = @CheckInTime, Price = @Price, CustomerId = @CustomerId WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@CheckInTime", entity.CheckInTime);
                    command.Parameters.AddWithValue("@Price", entity.Price);
                    command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);

                    command.ExecuteNonQuery();
                }
            }
            // Updates entity in collection
            var existingEntity = Entries.FirstOrDefault(e => e.Id == entity.Id);
            if (existingEntity != null)
            {
                int index = Entries.IndexOf(existingEntity);
                Entries[index] = entity;
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
            };
        }
    }
}
