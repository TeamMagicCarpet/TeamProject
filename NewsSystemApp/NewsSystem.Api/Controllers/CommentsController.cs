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
    public class CommentsController : ApiController
    {
        private readonly IRepository<Comment> commentRepository;
        private readonly IRepository<Article> articleRepository;
        private readonly IRepository<User> userRepository;

        public CommentsController(IRepository<Comment> commentRepository,
            IRepository<Article> articleRepository, IRepository<User> userRepository)
        {
            this.commentRepository = commentRepository;
            this.articleRepository = articleRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<CommentDetailsModel> GetArticleComments(int articleId)
        {
            var commentEntities = this.commentRepository.All().Where(c => c.Article.ArticleId == articleId);
            var commentModels =
                from commentEntity in commentEntities
                select new CommentDetailsModel()
                {
                    Content = commentEntity.Content,
                    Article = commentEntity.Article,
                    User = commentEntity.User,
                    Answers = commentEntity.Answers
                };
            return commentModels.ToList();
        }

        [HttpPost]
        public HttpResponseMessage PostComment(CommentModel comment, int articleId, int authorId)
        {
            Article article = articleRepository.Get(articleId);
            User author = userRepository.Get(authorId);
            var entityToAdd = new Comment()
            {
                Content = comment.Content,
                Article = article,
                User = author
            };

            var createEntity = this.commentRepository.Add(entityToAdd);

            var createdModel = new CommentModel()
            {
                CommentId = createEntity.CommentId,
                Content = createEntity.Content,
                Article = createEntity.Article,
                User = createEntity.User
            };

            var response = Request.CreateResponse<CommentModel>(HttpStatusCode.Created, createdModel);
            var resourceLink = Url.Link("DefaultApi", new { CommentId = createdModel.CommentId });
            response.Headers.Location = new Uri(resourceLink);

            return response;
        }
    }
}
