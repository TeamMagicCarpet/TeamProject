using System.Collections.Generic;

namespace NewsSystem.Models
{
    public class Comment
    {
        private ICollection<Comment> answers;

        public int CommentId { get; set; }
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public int AuthorId { get; set; }
        public bool IsSubComment { get; set; }
        public string UserName { get; set; }
        
        public Comment()
        {
            this.answers = new HashSet<Comment>();
        }

        public virtual ICollection<Comment> Answers
        {
            get
            {
                return this.answers;
            }
            set
            {
                this.answers = value;
            }
        }
    }
}
