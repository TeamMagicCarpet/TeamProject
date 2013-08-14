using System;

namespace NewsSystem.Models
{
    public class Vote
    {
        private int voteValue;

        public int VoteId { get; set; }
        public virtual User User { get; set; }
        public virtual Article Article { get; set; }

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
    }
}
