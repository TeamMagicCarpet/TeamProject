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
        private static IRepository<Comment> commentRepository = new EfDbRepository<Comment>(dbContext);
        private static IRepository<Vote> voteRepository = new EfDbRepository<Vote>(dbContext);

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
            if (serviceType == typeof(CommentsController))
            {
                return new CommentsController(commentRepository);
            }
            if (serviceType == typeof(VotesController))
            {
                return new VotesController(voteRepository);
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