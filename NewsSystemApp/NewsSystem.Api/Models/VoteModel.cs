using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsSystem.Models;

namespace NewsSystem.Api.Models
{
    public class VoteModel
    {
        public int VoteId { get; set; }

        public virtual User User { get; set; }

        public virtual Article Article { get; set; }

        public int Value { get; set; }

        public int UserId { get; set; }

        public int ArticleId { get; set; }
    }
}