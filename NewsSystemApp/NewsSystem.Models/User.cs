using System.Collections.Generic;

namespace NewsSystem.Models
{
    public class User
    {
        private ICollection<Article> articles;
        private ICollection<Comment> comments;

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public User()
        {
            this.articles = new HashSet<Article>();
            this.comments = new HashSet<Comment>();
        }

        public virtual ICollection<Article> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
