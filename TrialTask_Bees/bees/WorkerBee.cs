using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialTask_Bees
{
    [Serializable]
    public class WorkerBee : Bee
    {
        public WorkerBee() { }
        public WorkerBee(int counter)
        {
            Name = string.Format("Worker Bee{0}", counter);
            Type = BeeTypes.WorkerBee;
        }        
    }
}