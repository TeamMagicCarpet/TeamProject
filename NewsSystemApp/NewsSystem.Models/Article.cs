using System;
using System.Collections.Generic;

namespace NewsSystem.Models
{
    /// <summary>
    /// articles
    /// </summary>
    public class Article
    {
        private ICollection<string> images;
        private ICollection<Vote> votes;
        private ICollection<Comment> comments;

        public int ArticleId { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Content { get; set; }
        public virtual User Author { get; set; }

        public Article()
        {
            this.images = new HashSet<string>();
            this.votes = new HashSet<Vote>();
            this.comments = new HashSet<Comment>();
        }

        public virtual ICollection<string> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

    }
}
