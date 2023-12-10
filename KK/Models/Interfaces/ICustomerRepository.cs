using KK.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KK.Models.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Customer GetCustomer(int id);
        IEnumerable<Customer> GetCustomersWithMembershipsAndEntries();
    }
}
