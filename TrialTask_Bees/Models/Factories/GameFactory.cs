using System;
using System.Collections.Generic;
using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees.Factories
{
    public static class GameFactory
    {
        public static T CreateGame<T>(List<IGameEntityObjectInfo> objectsInfo) where T : IGame
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { objectsInfo });
        }
    }
}