using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int ParrentID { get; set; }
        public string Content { get; set; }

        public virtual User user { get;  set; }
        public virtual Article article { get; set; }
    }
}
