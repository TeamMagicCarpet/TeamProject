﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsSystem.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace NewsSystem.Repositories
{
    public class EfDbRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext dbContext;
        private readonly DbSet<T> entitySet;

        public EfDbRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entitySet = this.dbContext.Set<T>();
        }

        public T Add(T item)
        {
            DbEntityEntry entry = this.dbContext.Entry(item);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.entitySet.Add(item);
            }
            
            this.dbContext.SaveChanges();
            return item;
        }

        public void Update(int id, T item)
        {
            DbEntityEntry entry = this.dbContext.Entry(item);
            if (entry.State == EntityState.Detached)
            {
                this.entitySet.Attach(item);
            }

            entry.State = EntityState.Modified;

            this.dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = this.Get(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry entry = this.dbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.entitySet.Attach(entity);
                this.entitySet.Remove(entity);
            }

            this.dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return this.entitySet.Find(id);
        }

        public IQueryable<T> All()
        {
            return this.entitySet;
        }


        public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, int, bool>> predicate)
        {
            return this.entitySet.Where(predicate);
        }
    }
}
