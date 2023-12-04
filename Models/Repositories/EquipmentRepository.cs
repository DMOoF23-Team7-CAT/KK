using System;
using System.Collections.Generic;
using System.Data;
using KK.Models.Entities;
using KK.Models.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KK.Models.Repositories
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly string _connectionString;

        public EquipmentRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Equipment entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO kk_EQUIPMENT (Name, Price, EntryId) VALUES (@Name, @Price, @EntryId); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Price", entity.Price);
                    command.Parameters.AddWithValue("@EntryId", entity.Entry.Id); // Assuming Entry is a navigation property

                    // Execute the SQL command and get the inserted ID
                    entity.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public IEnumerable<Equipment> GetAll()
        {
            List<Equipment> equipmentList = new List<Equipment>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_EQUIPMENT", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            equipmentList.Add(MapDataToEquipment(reader));
                        }
                    }
                }
            }

            return equipmentList;
        }

        public Equipment GetById(int id)
        {
            Equipment equipment = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_EQUIPMENT WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            equipment = MapDataToEquipment(reader);
                        }
                    }
                }
            }

            return equipment;
        }

        public void Remove(Equipment entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM kk_EQUIPMENT WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Equipment entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE kk_EQUIPMENT SET Name = @Name, Price = @Price, EntryId = @EntryId WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Price", entity.Price);
                    command.Parameters.AddWithValue("@EntryId", entity.Entry.Id); // Assuming Entry is a navigation property

                    command.ExecuteNonQuery();
                }
            }
        }

        private static Equipment MapDataToEquipment(SqlDataReader reader)
        {
            return new Equipment
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
