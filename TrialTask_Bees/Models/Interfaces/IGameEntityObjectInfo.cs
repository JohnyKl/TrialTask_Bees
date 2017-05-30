using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrialTask_Bees.Models.Interfaces
{
    public interface IGameEntityObjectInfo
    {
        IGameEntityParameter Parameter { get; set; }
        Type Type { get; set; }
        int Number { get; set; }
    }
}
