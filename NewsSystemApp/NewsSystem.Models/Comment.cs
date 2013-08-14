using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int ParrentId { get; set; }
        public string Content { get; set; }

        public virtual User User { get;  set; }
        public virtual Article Article { get; set; }
    }
}
