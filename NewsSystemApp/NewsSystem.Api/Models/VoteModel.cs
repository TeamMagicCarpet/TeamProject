using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Api.Models
{
    public class VoteModel
    {
        public int VoteId { get; set; }

        public virtual UserModel User { get; set; }

        public virtual ArticleModel Article { get; set; }

        public int Value { get; set; }
    }
}