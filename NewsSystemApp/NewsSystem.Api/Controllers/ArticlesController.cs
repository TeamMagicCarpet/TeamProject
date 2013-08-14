using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsSystem.Models;
using NewsSystem.Repositories;
using NewsSystem.Api.Models;

namespace NewsSystem.Api.Controllers
{
    public class ArticlesController : ApiController
    {
        private IRepository<Article> articleRepository;

        public ArticlesController(IRepository<Article> articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        [HttpGet]
        public IEnumerable<ArticleModel> GetAll()
        {
            var articleEnteties = this.articleRepository.All();

            var articleModels =
                from articleEntity in articleEnteties
                select new ArticleModel()
                {
                    ArticleId = articleEntity.ArticleId,
                    Title = articleEntity.Title,
                    Content = articleEntity.Content,
                    CreationDate = articleEntity.CreationDate,
                    Rating = articleEntity.Votes.Average(x => x.Value),
                    CommentsCount = articleEntity.Comments.Count()
                };
            return articleModels.ToList();
        }

        [HttpGet]
        public ArticleDetailsModel GetSingleArticle(int id)
        {
            var articleEntity = this.articleRepository.Get(id);
            var articleModel = new ArticleDetailsModel()
            {
                ArticleId  = articleEntity.ArticleId,
                Title = articleEntity.Title,
                Content = articleEntity.Content,
                CreationDate = articleEntity.CreationDate,
                Comments = articleEntity.Comments,
                Votes = articleEntity.Votes,
                Images = articleEntity.Images,
            };

            return articleModel;
        }

        [HttpPost]
        public HttpResponseMessage PostArticles(ArticleModel model)
        {
            var entityToAdd = new Article()
            {
                Title = model.Title,
                Content = model.Content,
                CreationDate = DateTime.Now,
            };

            var createEntity = this.articleRepository.Add(entityToAdd);

            var createdModel = new ArticleModel()
            {
                ArticleId = createEntity.ArticleId,
                Title = createEntity.Title,
                Content = createEntity.Content,
                CreationDate = createEntity.CreationDate,
                Rating = createEntity.Votes.Average(x=>x.Value),
                CommentsCount = createEntity.Comments.Count()
            };

            var response = Request.CreateResponse<ArticleModel>(HttpStatusCode.Created, createdModel);
            var resourceLink = Url.Link("DefaultApi", new { UserId = createdModel.ArticleId });
            response.Headers.Location = new Uri(resourceLink);

            return response;
        }
    }
}
