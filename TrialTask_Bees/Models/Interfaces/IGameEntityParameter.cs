using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrialTask_Bees.Models.Interfaces
{
    public interface IGameEntityParameter
    {
        void IncreaseHealth();
        IGameEntityParameter Copy();

        int Health { get; set; }
        int HitPoints { get; set; }
    }
}
