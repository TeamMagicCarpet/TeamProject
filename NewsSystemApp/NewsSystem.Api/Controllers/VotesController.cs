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
    public class VotesController : ApiController
    {
        private readonly IRepository<Vote> voteRepository;
        private readonly IRepository<Article> articleRepository;
        private readonly IRepository<User> userRepository;

        public VotesController(IRepository<Vote> voteRepository, IRepository<Article> articleRepository, IRepository<User> userRepository)
        {
            this.articleRepository = articleRepository;
            this.userRepository = userRepository;
            this.voteRepository = voteRepository;
        }

        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage PostVote(VoteModel model)
        {
            var user = this.userRepository.Get(model.UserId);
            var article = this.articleRepository.Get(model.ArticleId);

            var entityToAdd = new Vote()
            {
                User = user,
                Article = article,
                Value = model.Value,
            };
            
            var createEntity = this.voteRepository.Add(entityToAdd);
            article.Votes.Add(createEntity);
            this.articleRepository.Update(article.ArticleId, article);

            var createdModel = new VoteModel()
            {
                VoteId = createEntity.VoteId,
                User = createEntity.User,
                Article = createEntity.Article,
                Value = createEntity.Value,
            };

            var response = Request.CreateResponse<VoteModel>(HttpStatusCode.Created, createdModel);
            var resourceLink = Url.Link("DefaultApi", new { VoteId = createdModel.VoteId });
            response.Headers.Location = new Uri(resourceLink);

            return response;
        }
    }
}