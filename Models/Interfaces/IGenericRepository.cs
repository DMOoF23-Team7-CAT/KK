using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KK.Models.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // CREATE
        void Add(T entity);
        //void AddRange(IEnumerable<T> entities);

        // READ
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        // UPDATE
        void Update(T entity);
        //void UpdateRange(IEnumerable<T> entities);

        // DELETE
        void Remove(T entity);
        //void RemoveRange(IEnumerable<T> entities);
    }
}
