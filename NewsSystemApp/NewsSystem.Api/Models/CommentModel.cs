using System.Collections.Generic;
using NewsSystem.Models;

namespace NewsSystem.Api.Models
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public Article Article { get; set; }
    }

    public class CommentDetailsModel : CommentModel
    {
        public virtual ICollection<CommentDetailsModel> Answers { get; set; }
    }
}