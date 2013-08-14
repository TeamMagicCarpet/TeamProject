using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using System.Data.Entity;
using NewsSystem.Repositories;
using NewsSystem.Models;
using NewsSystem.Api.Controllers;
using NewsSystem.Data;

namespace NewsSystem.Api.Resolvers
{
    public class DbDependencyResolver : IDependencyResolver
    {
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(UsersController))
            {
                return new UsersController(new EfDbRepository<User>(new NewsSystemContext()));
            }
            if (serviceType == typeof(ArticlesController))
            {
                return new ArticlesController(new EfDbRepository<Article>(new NewsSystemContext()));
            }
            if (serviceType == typeof(CommentsController))
            {
                return new CommentsController(new EfDbRepository<Comment>(new NewsSystemContext()),
                    new EfDbRepository<Article>(new NewsSystemContext()), new EfDbRepository<User>(new NewsSystemContext()));
            }
            if (serviceType == typeof(VotesController))
            {
                return new VotesController(new EfDbRepository<Vote>(new NewsSystemContext()));
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
        }
    }
}