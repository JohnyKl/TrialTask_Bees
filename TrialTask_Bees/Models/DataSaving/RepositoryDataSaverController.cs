using System;
using System.Threading;
using TrialTask_Bees.Interfaces;
using TrialTask_Bees.Repository;

namespace TrialTask_Bees.DataSaving
{
    public class RepositoryDataSaverController<K, F> : IDataSaverController where F : INumerable where K : IRepository<F>
    {     
        public void Save<T>(T obj)
        {
            if (obj is INumerable)
            {
                Monitor.Enter(LockObject);

                INumerable workedObj = (INumerable)obj;

                repo.Add((F)workedObj);
                Monitor.Exit(LockObject);
            }
            else
            {
                //TODO: add logging
            }
        }

        private static K repo = (K)Activator.CreateInstance(typeof(K));

        private static readonly object LockObject = new object();
    }
}