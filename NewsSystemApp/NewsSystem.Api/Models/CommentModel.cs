using System.Collections.Generic;

namespace NewsSystem.Api.Models
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public UserModel User { get; set; }
        public ArticleModel Article { get; set; }
    }

    public class CommentDetailsModel : CommentModel
    {
        public virtual ICollection<CommentDetailsModel> Answers { get; set; }
    }
}