﻿using NewsSystem.Models;
using System;
using System.Collections.Generic;

namespace NewsSystem.Api.Models
{
    public class ArticleModel
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public int CommentsCount { get; set; }

        public string Content { get; set; }

        public double Rating { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public ICollection<string> Images { get; set; }

        public string Image1 { get; set; }
    }

    public class ArticleDetailsModel : ArticleModel
    {
        public ICollection<Comment> Comments { get; set; }

    }
}