using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrialTask_Bees.Interfaces
{
    public interface IGameService
    {
        Dictionary<string, IGame> games { get; set; }
        void Add(string key, IGame game);
        IGame Get(string key);
        void Remove(string key);
        void Remove(IGame game);
        bool Save(string key, IDataSaverController controller);
    }
}
