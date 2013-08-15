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
        private IRepository<Article> articleRepository;

        public ArticlesController(IRepository<Article> articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        [HttpGet]
        [ActionName("getall")]
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
                    //Rating = articleEntity.Votes !=null? articleEntity.Votes.Average(x => x.Value) : 0,
                    //CommentsCount = articleEntity.Comments !=null? articleEntity.Comments.Count() : 0
                };
            return articleModels.ToList();
        }

        [HttpGet]
        [ActionName("getarticle")]
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
        [ActionName("create")]
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
                //Rating = createEntity.Votes!= null ? createEntity.Votes.Average(x=>x.Value) : 0,
                //CommentsCount = createEntity.Comments != null? createEntity.Comments.Count() : 0
            };

            PubnubAPI pubnub = new PubnubAPI(
                "pub-c-471827c9-62e3-42ac-8a02-524042ee4ba2",               // PUBLISH_KEY
                "sub-c-0cc9fc44-0531-11e3-a3d6-02ee2ddab7fe",               // SUBSCRIBE_KEY
                "sec-c-MGYwMDBlYjgtZmIxNC00N2Q0LThlNzgtZGVmZGMwNGE2YmJk",   // SECRET_KEY
                true                                                        // SSL_ON?
            );

            string channel = "newssytem-channel";

            pubnub.Publish(channel, createdModel.Title + " " + createdModel.CreationDate);

            var response = Request.CreateResponse<ArticleModel>(HttpStatusCode.Created, createdModel);
            var resourceLink = Url.Link("DefaultApi", new { ArticleId = createdModel.ArticleId });
            response.Headers.Location = new Uri(resourceLink);

            return response;
        }
    }
}
