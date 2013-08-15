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
        private IRepository<Vote> voteRepository;

        public VotesController(IRepository<Vote> voteRepository)
        {
            this.voteRepository = voteRepository;
        }

        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage PostVote(VoteModel model)
        {
            var entityToAdd = new Vote()
            {
                User = model.User,
                Article = model.Article,
                Value = model.Value,
            };

            var createEntity = this.voteRepository.Add(entityToAdd);

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