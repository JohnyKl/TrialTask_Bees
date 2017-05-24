using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialTask_Bees.bees
{
    public class BeesFactory
    {
        public static T CreateBee<T>(int beeId) where T : Bee
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { beeId });
        }

        public static T CreateBee<T>(int beeId, int health, int hitPoints) where T : Bee
        {
            T obj = CreateBee<T>(beeId);

            obj.Health = health;
            obj.HitPoints = hitPoints;

            return obj;
        }
    }
}