using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrialTask_Bees.Interfaces;

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
                //TODO: add logging
            }
        }
    }
}