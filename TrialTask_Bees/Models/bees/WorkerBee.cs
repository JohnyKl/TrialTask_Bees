using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialTask_Bees
{
    [Serializable]
    public class WorkerBee : Bee
    {
        public WorkerBee() : this(counter) { }
        public WorkerBee(int id) : base(id)
        {
            counter++;
            Name = "Worker";
        }

        public override void Dispose()
        {
            counter--;
        }

        private static int counter = 0;

    }
}