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
        private static DbContext dbContext = new NewsSystemContext();

        private static IRepository<User> userRepository = new EfDbRepository<User>(dbContext);
        private static IRepository<Article> articleRepository = new EfDbRepository<Article>(dbContext);

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(UsersController))
            {
                return new UsersController(userRepository);
            }
            if (serviceType == typeof(ArticlesController))
            {
                return new ArticlesController(articleRepository);
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