﻿using System;
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
        private readonly IRepository<Article> articleRepository;
        private readonly IRepository<User> userRepository;

        public ArticlesController(IRepository<Article> articleRepository, IRepository<User> userRepository)
        {
            this.articleRepository = articleRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [ActionName("getall")]
        public IEnumerable<ArticleModel> GetAll()
        {
            var articleEnteties = this.articleRepository.All();

            var articleModels =
                from articleEntity in articleEnteties
                orderby articleEntity.CreationDate descending
                select new ArticleModel()
                {
                    ArticleId = articleEntity.ArticleId,
                    Title = articleEntity.Title,
                    Content = articleEntity.Content,
                    CreationDate = articleEntity.CreationDate,
                    Rating = articleEntity.Votes.Any()? articleEntity.Votes.Average(x => x.Value) : 0,
                    CommentsCount = articleEntity.Comments.Any()? articleEntity.Comments.Count() : 0,
                    AuthorId = articleEntity.Author.UserId,
                    AuthorName = articleEntity.Author.UserName
                };

            return articleModels.ToList();
        }

        [HttpGet]
        [ActionName("getarticle")]
        public ArticleDetailsModel GetSingleArticle(string token)
        {
            int articleId = int.Parse(token);
            var articleEntity = this.articleRepository.Get(articleId);

            if (articleEntity == null)
            {
                throw new ArgumentException("ArticleId does not exist");
            }

            var articleModel = new ArticleDetailsModel()
            {
                ArticleId  = articleEntity.ArticleId,
                Title = articleEntity.Title,
                Content = articleEntity.Content,
                CreationDate = articleEntity.CreationDate,
                Comments = articleEntity.Comments.Any() ? articleEntity.Comments.Where(x => x.IsSubComment == false).ToList() : articleEntity.Comments,
                Rating = articleEntity.Votes.Any() ? articleEntity.Votes.Average(x => x.Value) : 0,
                Images = articleEntity.Images,
                AuthorId = articleEntity.Author.UserId,
                AuthorName = articleEntity.Author.UserName
            };

            return articleModel;
        }

        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage PostArticles(ArticleModel model)
        {
            User author = userRepository.Get(model.AuthorId);

            var entityToAdd = new Article()
            {
                Title = model.Title,
                Content = model.Content,
                Author = author,
                //Images = model.Images,
                CreationDate = DateTime.Now,
                Image1 = model.Image1
            };

            var createEntity = this.articleRepository.Add(entityToAdd);

            author.Articles.Add(createEntity);

            this.userRepository.Update(author.UserId, author);

            var createdModel = new ArticleModel()
            {
                ArticleId = createEntity.ArticleId,
                Title = createEntity.Title,
                Content = createEntity.Content,
                CreationDate = createEntity.CreationDate,
                Rating = createEntity.Votes.Any() ? createEntity.Votes.Average(x=>x.Value) : 0,
                CommentsCount = createEntity.Comments.Count(),
                //Images = createEntity.Images
                Image1 = createEntity.Image1
            };

            PubnubAPI pubnub = new PubnubAPI(
                "pub-c-471827c9-62e3-42ac-8a02-524042ee4ba2",               // PUBLISH_KEY
                "sub-c-0cc9fc44-0531-11e3-a3d6-02ee2ddab7fe",               // SUBSCRIBE_KEY
                "sec-c-MGYwMDBlYjgtZmIxNC00N2Q0LThlNzgtZGVmZGMwNGE2YmJk",   // SECRET_KEY
                true                                                        // SSL_ON?
            );

            string channel = "newssytem-channel";
            string message = string.Format("{0} {1}", createdModel.Title, createdModel.CreationDate);
            pubnub.Publish(channel, message);

            var response = Request.CreateResponse<ArticleModel>(HttpStatusCode.Created, createdModel);
            var resourceLink = Url.Link("DefaultApi", new { ArticleId = createdModel.ArticleId });
            response.Headers.Location = new Uri(resourceLink);

            return response;
        }
    }
}
