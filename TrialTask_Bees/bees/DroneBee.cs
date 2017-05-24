using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialTask_Bees
{
    [Serializable]
    public class DroneBee : Bee
    {
        public DroneBee() : this(counter) { }

        public DroneBee(int id) : base(id)
        {
            counter++;
            Type = BeeTypes.Drone;
        }             
           
        private static int counter = 0;
    }
}