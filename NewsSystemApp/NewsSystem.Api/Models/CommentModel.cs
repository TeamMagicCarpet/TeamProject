using System.Collections.Generic;
using NewsSystem.Models;

namespace NewsSystem.Api.Models
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public int ArticleId { get; set; }
        public bool IsSubComment { get; set; }
        public int ParrentCommentId { get; set; }
        public string Author { get; set; }
         public string Article { get; set; }
    }

    public class CommentDetailsModel : CommentModel
    {
       
       
        public virtual ICollection<Comment> Answers { get; set; }
    }
}