﻿using NewsSystem.Models;
using System;
using System.Collections.Generic;

namespace NewsSystem.Api.Models
{
    public class ArticleModel
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public int VotesCount { get; set; }

        public DateTime CreationDate { get; set; }

        public int CommentsCount { get; set; }

        public string Content { get; set; }
    }

    public class ArticleDetailsModel : ArticleModel
    {
        public ICollection<Comment> Comments { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}