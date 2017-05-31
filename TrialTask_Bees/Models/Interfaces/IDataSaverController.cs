using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrialTask_Bees.Interfaces
{
    public interface IDataSaverController
    {
        void Save<T>(T obj);
    }
}
