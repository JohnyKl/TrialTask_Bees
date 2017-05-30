using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrialTask_Bees.DataSaving;
using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees.Models.Interfaces
{
    public interface IGame : INumerable, IMemorySaveable<IGame>
    {
        int HitCount { get; set; }
        int TotalKilled { get; set; }
        List<IGameEntityObjectInfo> GameObjectsParameters { get; set; }

        void Create();
        void Restart();
        void Hit();
        void Save(IDataSaverController controller);
        string AlivesToString();
        bool IsGameOver();
    }
}
