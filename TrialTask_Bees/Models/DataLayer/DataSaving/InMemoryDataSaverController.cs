using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrialTask_Bees.Interfaces;
using TrialTask_Bees.Logging;

namespace TrialTask_Bees.DataSaving
{
    public class InMemoryDataSaverController : IDataSaverController
    {
        public void Save<T>(T obj)
        {
            if(obj is IMemorySaveable<T>)
            {
                IMemorySaveable<T> workedObj = obj as IMemorySaveable<T>;

                workedObj.SavedCopy = workedObj.Copy();
            }
            else
            {
                Logger.Log.Error(string.Format("InMemoryDataSaverController cannot save an object of class {0}"+ 
                    "because it don`t implement the IMemorySaveable<T>", typeof(T).ToString()));
            }
        }
    }
}