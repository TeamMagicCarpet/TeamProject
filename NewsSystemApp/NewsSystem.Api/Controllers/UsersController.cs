using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsSystem.Repositories;
using NewsSystem.Models;
using NewsSystem.Api.Models;

namespace NewsSystem.Api.Controllers
{
    public class UsersController : ApiController
    {
        private IRepository<User> userRepository;

        public UsersController(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [ActionName("all")]
        public IEnumerable<UserModel> GetAll()
        {
            var userEntities = this.userRepository.All();
            var userModels =
                from userEntity in userEntities
                select new UserModel()
                {
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    UserName = userEntity.UserName,
                    Email = userEntity.Email,
                    ArticleCount = userEntity.Articles.Count(),
                };
            return userModels.ToList();
        }
    }
}
