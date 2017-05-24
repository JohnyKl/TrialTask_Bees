using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialTask_Bees
{
    [Serializable]
    public class QueenBee : Bee
    {
        public QueenBee() : this(counter) { }
        public QueenBee(int id) : base(id)
        {
            if (counter > 1) throw new ArgumentOutOfRangeException("Only one Queen Bee is allowed!");
            counter++;
            Type = BeeTypes.Queen;
        }

        private static int counter = 0;
    }
}