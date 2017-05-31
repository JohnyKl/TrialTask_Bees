using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrialTask_Bees.Interfaces
{
    public interface IGameEntityObjectInfo
    {
        int Health { get; set; }
        int HitPoint { get; set; }
        Type Type { get; set; }
        int Number { get; set; }
    }
}
