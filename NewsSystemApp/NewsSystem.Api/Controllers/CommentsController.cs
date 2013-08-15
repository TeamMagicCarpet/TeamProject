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
        [ActionName("getallfrom")]
        public IEnumerable<CommentDetailsModel> GetArticleComments(string token)
        {
            int articleId = int.Parse(token);
            var commentEntities = this.commentRepository.All().Where(c => c.Article.ArticleId == articleId);
            var commentModels =
                from commentEntity in commentEntities
                select new CommentDetailsModel()
                {
                    Content = commentEntity.Content,
                    Article = commentEntity.Article.Title,
                    Author = commentEntity.User.UserName,
                    Answers = commentEntity.Answers
                };
            return commentModels.ToList();
        }

        [HttpGet]
        [ActionName("getall")]
        public IEnumerable<CommentDetailsModel> GetAllComments()
        {
            var commentEntities = this.commentRepository.All();
            var commentModels =
                from commentEntity in commentEntities
                select new CommentDetailsModel()
                {
                    Content = commentEntity.Content,
                    Article = commentEntity.Article.Title,
                    Author = commentEntity.User.UserName,
                    Answers = commentEntity.Answers
                };
            return commentModels.ToList();
        }

        [HttpGet]
        [ActionName("getcomment")]
        public CommentDetailsModel GetComment(string token)
        {
            int commentId = int.Parse(token);
            var commentEntity = this.commentRepository.All().Where(c => c.CommentId == commentId).FirstOrDefault();
            var commentModels = new CommentDetailsModel()
                {
                    Content = commentEntity.Content,
                    Article = commentEntity.Article.Title,
                    Author = commentEntity.User.UserName,
                    Answers = commentEntity.Answers
                };
            return commentModels;
        }

        [HttpPost]
        [ActionName("create")]
        public HttpResponseMessage PostComment(CommentModel comment)
        {
            Article article = articleRepository.Get(comment.ArticleId);
            User author = userRepository.Get(comment.AuthorId);

            var entityToAdd = new Comment()
            {
                Content = comment.Content,
                Article = article,
                User = author,
            };

            var createEntity = this.commentRepository.Add(entityToAdd);


            Comment parrentComment;
            if (comment.IsSubComment)
            {
                parrentComment = commentRepository.Get(comment.ParrentCommentId);
                parrentComment.Answers.Add(createEntity);
                this.commentRepository.Update(parrentComment.CommentId, parrentComment);
            }
            
            article.Comments.Add(createEntity);
            this.articleRepository.Update(article.ArticleId, article);

            author.Comments.Add(createEntity);
            this.userRepository.Update(author.UserId, author);

            var createdModel = new CommentDetailsModel()
            {
                CommentId = createEntity.CommentId,
                Content = createEntity.Content,
                Article = createEntity.Article.Title,
                Author = createEntity.User.UserName,
                Answers = createEntity.Answers
            };

            var response = Request.CreateResponse<CommentModel>(HttpStatusCode.Created, createdModel);
            var resourceLink = Url.Link("DefaultApi", new { CommentId = createdModel.CommentId });
            response.Headers.Location = new Uri(resourceLink);

            return response;
        }
    }
}
