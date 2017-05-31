using System;
using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees.Factories
{
    public class BeesFactory
    {
        //public static T CreateBee<T>(int beeId) where T : Bee
        //{
        //    return (T)Activator.CreateInstance(typeof(T), new object[] { beeId });
        //}

        //public static T CreateBee<T>(int beeId, IGameEntityParameter parameters) where T : Bee
        //{
        //    T obj = CreateBee<T>(beeId);

        //    obj.Parameters = parameters;

        //    return obj;
        //}

        public static Bee CreateBee(int beeId, IGameEntityObjectInfo objectInfo)
        {
            Bee newBee = (Bee)Activator.CreateInstance(objectInfo.Type, new object[] { beeId });

            newBee.Health = objectInfo.Health;
            newBee.HitPoints = objectInfo.HitPoint;
            
            return newBee;
        }
    }
}