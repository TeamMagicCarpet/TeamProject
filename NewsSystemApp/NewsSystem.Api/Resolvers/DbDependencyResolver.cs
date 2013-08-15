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
            else if (serviceType == typeof(ArticlesController))
            {
                NewsSystemContext dbContext = new NewsSystemContext();
                return new ArticlesController(new EfDbRepository<Article>(dbContext), new EfDbRepository<User>(dbContext));
            }
            else if (serviceType == typeof(CommentsController))
            {
                NewsSystemContext dbContext = new NewsSystemContext();
                return new CommentsController(
                    new EfDbRepository<Comment>(dbContext),
                    new EfDbRepository<Article>(dbContext), 
                    new EfDbRepository<User>(dbContext));
            }
            else if (serviceType == typeof(VotesController))
            {
                NewsSystemContext dbContext = new NewsSystemContext();
                return new VotesController(new EfDbRepository<Vote>(dbContext), new EfDbRepository<Article>(dbContext), new EfDbRepository<User>(dbContext));
            }
            else if (serviceType == typeof(ImagesController))
            {
                return new ImagesController(new EfDbRepository<Image>(new NewsSystemContext()));
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