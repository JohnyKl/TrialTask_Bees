using System.Collections.Generic;
using System.Linq;
using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees.Repository
{
    public class InMemoryRepository<T> : IRepository<T> where T : INumerable 
    {
        private readonly IList<INumerable> entities = new List<INumerable>();

        public void Add(T game)
        {
            T current = Get(game.Id);

            if(current != null)
            {
                entities.Remove(current);
            }            

            entities.Add(game);
        }

        public void Remove(int id)
        {
            T current = Get(id);

            if (current != null)
            {
                entities.Remove(current);
            }
        }

        public T Get(int id)
        {
            return entities.OfType<T>().SingleOrDefault(e => e.Id == id);
        }        
    }
}