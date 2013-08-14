using System;
using System.Linq;
using System.Linq.Expressions;

namespace NewsSystem.Repositories
{
    public interface IRepository<T>
    {
        T Add(T item);
        void Update(int id, T item);
        void Delete(int id);
        void Delete(T item);
        T Get(int id);
        IQueryable<T> All();
        IQueryable<T> Find(Expression<Func<T, int, bool>> predicate);
    }
}
