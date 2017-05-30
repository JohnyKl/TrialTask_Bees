using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrialTask_Bees.Models.Interfaces;

namespace TrialTask_Bees.Models.Factories
{
    public static class GameFactory
    {
        public static T CreateGame<T>(List<IGameEntityObjectInfo> objectsInfo) where T : IGame
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { objectsInfo });
        }
    }
}