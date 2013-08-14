using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Models
{
    public class Vote
    {
        public int VoteId { get; set; }

        private int voteValue;
        public int Value
        {
            get
            {
                return this.voteValue;
            }
            set
            {
                if (0 > value && value > 10)
                {
                    throw new ArgumentOutOfRangeException("The voteValue value must be in range [0 - 10]!");
                }

                this.voteValue = value;
            }
        }

        public virtual User User { get; set; }
        public virtual Article Article { get; set; }
    }
}
