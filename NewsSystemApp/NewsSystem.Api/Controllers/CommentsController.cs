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

        public CommentsController(IRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        [HttpGet]
        public IEnumerable<CommentDetailsModel> GetAll()
        {
            var commentEntities = this.commentRepository.All();
            var commentModels =
                from commentEntity in commentEntities
                select new CommentDetailsModel()
                {
                    User = commentEntity.User,

                    //UserId = commentEntity.UserId,
                    //FirstName = commentEntity.FirstName,
                    //LastName = commentEntity.LastName,
                    //UserName = commentEntity.User,
                };
            return commentModels.ToList();
        }
    }
}
