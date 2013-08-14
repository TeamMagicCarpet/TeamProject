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
        public IEnumerable<UserModel> GetAll()
        {
            var userEntities = this.userRepository.All();
            var userModels =
                from userEntity in userEntities
                select new UserModel()
                {
                    UserId = userEntity.UserId,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    UserName = userEntity.UserName,
                    Email = userEntity.Email,
                    ArticleCount = userEntity.Articles.Count(),
                };
            return userModels.ToList();
        }

        [HttpGet]
        public UserDetailsModel GetSingleUser(int id)
        {
            var userEntity = this.userRepository.Get(id);
            var userModel = new UserDetailsModel() 
            {
                UserId = userEntity.UserId,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                UserName = userEntity.UserName,
                Email = userEntity.Email,
                Articles = userEntity.Articles,
            };

            return userModel;
        }

        public HttpResponseMessage PostUser(UserModel model)
        { 
            var entityToAdd = new User()
            {
                FirstName = model.FirstName,
                LastName = model.FirstName,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
            };

            var createEntity = this.userRepository.Add(entityToAdd);

            var createdModel = new UserModel()
            {
                UserId = createEntity.UserId,
                UserName = createEntity.UserName,
                FirstName = createEntity.FirstName,
                LastName = createEntity.LastName,
                Email = createEntity.Email
            };

            var response = Request.CreateResponse<UserModel>(HttpStatusCode.Created, createdModel);
            var resourceLink = Url.Link("DefaultApi", new { UserId = createdModel.UserId });
            response.Headers.Location = new Uri(resourceLink);

            return response;
        }


    }
}
