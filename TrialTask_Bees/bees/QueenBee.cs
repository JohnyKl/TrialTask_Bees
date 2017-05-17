using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialTask_Bees
{
    [Serializable]
    public class QueenBee : Bee
    {
        public QueenBee() { }
        public QueenBee(int counter)
        {
            if (counter > 1) throw new ArgumentOutOfRangeException("Only one Queen Bee is allowed!");
            
            Name = string.Format("Queen Bee{0}", counter);
            Type = BeeTypes.QueenBee;
        }
        
    }
}