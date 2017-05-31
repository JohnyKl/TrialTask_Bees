using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees.Interfaces
{
    public interface IRepository<T> where T : INumerable
    {
        void Add(T obj);
        void Remove(int id);
        T Get(int id);
    }
}
