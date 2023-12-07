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
    public class ServiceItemRepository : IServiceItemRepository
    {
        private readonly string _connectionString;
        public ObservableCollection<ServiceItem> ServiceItems { get; set; }
        public ServiceItemRepository()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(ServiceItem entity)
        {
            // adds entity to database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO kk_SERVICEITEM (Name, EntryId) VALUES (@Name, @EntryId); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@EntryId", entity.EntryId);

                    // Execute the SQL command and get the inserted ID
                    entity.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            // adds entity to collectiuon
            if(!ServiceItems.Contains(entity)) { ServiceItems.Add(entity); }
        }

        public IEnumerable<ServiceItem> GetAll()
        {
            ServiceItems = new ObservableCollection<ServiceItem>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_SERVICEITEM", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ServiceItems.Add(MapDataToServiceItem(reader));
                        }
                    }
                }
            }

            return ServiceItems;
        }

        public ServiceItem GetById(int id)
        {
            ServiceItem serviceItem = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM kk_SERVICEITEM WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            serviceItem = MapDataToServiceItem(reader);
                        }
                    }
                }
            }

            return serviceItem;
        }

        public void Remove(ServiceItem entity)
        {
            // Removes entity from databse
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM kk_SERVICEITEM WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.ExecuteNonQuery();
                }
            }
            // removes entity from Collection
            if (ServiceItems.Contains(entity)) { ServiceItems.Add(entity); }
        }

        public void Update(ServiceItem entity)
        {
            // updates databse
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE kk_SERVICEITEM SET Name = @Name, EntryId = @EntryId WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@EntryId", entity.EntryId);

                    command.ExecuteNonQuery();
                }
            }
            // Adds entity To Collection or Updates it
            if (!ServiceItems.Contains(entity))
            {
                ServiceItems.Add(entity);
            }
            else
            {
                var existingEntity = ServiceItems.FirstOrDefault(e => e.Id == entity.Id);
                if (existingEntity != null)
                {
                    int index = ServiceItems.IndexOf(existingEntity);
                    ServiceItems[index] = entity;
                }
            }
        }

        private static ServiceItem MapDataToServiceItem(SqlDataReader reader)
        {
            return new ServiceItem
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                EntryId = Convert.ToInt32(reader["EntryId"])
            };
        }

    }
}
