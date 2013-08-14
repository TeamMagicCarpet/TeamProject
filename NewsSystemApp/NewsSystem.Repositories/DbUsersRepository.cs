using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsSystem.Models;
using System.Data.Entity;

namespace NewsSystem.Repositories
{
    public class DbUsersRepository : IRepository<User>
    {
        private DbContext dbContext;
        private DbSet<User> entitySet;

        public DbUsersRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<User>();
        }

        public User Add(User item)
        {
            this.entitySet.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public User Update(int id, User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(User item)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<User> All()
        {
            return this.entitySet;
        }
    }
}
