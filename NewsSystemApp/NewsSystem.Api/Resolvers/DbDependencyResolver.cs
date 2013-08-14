using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using System.Data.Entity;
using NewsSystem.Repositories;
using NewsSystem.Models;
using NewsSystem.Api.Controllers;

namespace NewsSystem.Api.Resolvers
{
    public class DbDependencyResolver : IDependencyResolver
    {
        //private static DbContext usersContent = new NewsSystemContext();
        //private static IRepository<User> repository = new DbUsersRepository(NewsSystemContext);

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(UsersController))
            {
                //return new UsersController();
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