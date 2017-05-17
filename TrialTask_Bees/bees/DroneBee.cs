using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialTask_Bees
{
    [Serializable]
    public class DroneBee : Bee
    {
        public DroneBee() { }
        public DroneBee(int counter)
        {
            Name = string.Format("Drone Bee{0}", counter);
            Type = BeeTypes.DroneBee;
        }                
    }
}