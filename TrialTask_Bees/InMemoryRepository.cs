using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrialTask_Bees
{
    public class InMemoryRepository
    {
        private readonly IList<BeesGame> entities = new List<BeesGame>();

        public void Add(BeesGame game)
        {
            BeesGame current = Get(game.Id);

            if(current != null)
            {
                entities.Remove(current);
            }            

            entities.Add(game);
        }

        public void Remove(int id)
        {
            BeesGame current = Get(id);

            if (current != null)
            {
                entities.Remove(current);
            }
        }

        public BeesGame Get(int id)
        {
            return entities.OfType<BeesGame>().SingleOrDefault(e => e.Id == id);
        }

        public IList<BeesGame> GetAll()
        {
            return entities.OfType<BeesGame>().ToList();
        }

        public IQueryable<BeesGame> Query()
        {
            return GetAll().AsQueryable();
        }
    }
}