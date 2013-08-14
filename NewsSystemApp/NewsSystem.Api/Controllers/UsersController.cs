using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsSystem.Repositories;
using NewsSystem.Models;
using NewsSystem.Api.Models;
using System.Text;

namespace NewsSystem.Api.Controllers
{
    public class UsersController : ApiController
    {
        private IRepository<User> userRepository;
        private static Random rand;

        private const string SessionKeyChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int SessionKeyLen = 50;


        public UsersController(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
            rand = new Random();
        }

        [HttpGet]
        [ActionName("getall")]
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
        [ActionName("getuser")]
        public UserDetailsModel GetUser(string sessionKey)
        {
            var userEntity = this.userRepository.Get(int.Parse(sessionKey));
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

        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage RegisterUser(UserModel model)
        { 
            var entityToAdd = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
              
            };

            var createEntity = this.userRepository.Add(entityToAdd);

            var createdModel = new UserModel()
            {
                UserId = createEntity.UserId,
                UserName = createEntity.UserName,
                FirstName = createEntity.FirstName,
                LastName = createEntity.LastName,
                Email = createEntity.Email,
                SessionKey = GenerateSessionKey(createEntity.UserId) 
            };

            var response = Request.CreateResponse<UserModel>(HttpStatusCode.Created, createdModel);
            var resourceLink = Url.Link("DefaultApi", new { UserId = createdModel.UserId });
            response.Headers.Location = new Uri(resourceLink);

            return response;
        }

        [HttpPost]
        [ActionName("login")]
        public HttpResponseMessage LoginUser(UserModel model)
        {
            var entityToAdd = new User()
            {
                UserName = model.UserName,
                Password = model.Password,
            };

            var findEntity = this.userRepository.All().Where(x=> x.Password == model.Password).FirstOrDefault();

            if (findEntity == null)
            {
                var errorResponse = Request.CreateResponse(HttpStatusCode.BadRequest, "User does not exist.");

                return errorResponse;
            }
            var createdModel = new UserModel()
            {
                UserId = findEntity.UserId,
                UserName = findEntity.UserName,
                FirstName = findEntity.FirstName,
                LastName = findEntity.LastName,
                Email = findEntity.Email,
                SessionKey = GenerateSessionKey(findEntity.UserId)
            };

            findEntity.SessionKey = createdModel.SessionKey;
            this.userRepository.Update(findEntity.UserId, findEntity);

            var response = Request.CreateResponse<UserModel>(HttpStatusCode.OK, createdModel);
            var resourceLink = Url.Link("DefaultApi", new { UserId = createdModel.UserId });
            response.Headers.Location = new Uri(resourceLink);

            return response;
        }

        [HttpGet]
        [ActionName("logout")]
        public void LogoutUser(string sessionKey)
        { 
            var findEntity = this.userRepository.All().Where(x=> x.SessionKey == sessionKey).FirstOrDefault();
            if (findEntity == null)
	        {
                throw new ArgumentException("Session key has expired");
	        }

            findEntity.SessionKey = null;

            this.userRepository.Update(findEntity.UserId, findEntity);
        }

        private static string GenerateSessionKey(int userId)
        {
            StringBuilder keyChars = new StringBuilder(50);
            keyChars.Append(userId.ToString());
            while (keyChars.Length < SessionKeyLen)
            {
                int randomCharNum;
                lock (rand)
                {
                    randomCharNum = rand.Next(SessionKeyChars.Length);
                }
                char randomKeyChar = SessionKeyChars[randomCharNum];
                keyChars.Append(randomKeyChar);
            }
            string sessionKey = keyChars.ToString();
            return sessionKey;
        }
    }
}
