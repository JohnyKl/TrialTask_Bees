
using System.Collections.Generic;
using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees.Interfaces
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
