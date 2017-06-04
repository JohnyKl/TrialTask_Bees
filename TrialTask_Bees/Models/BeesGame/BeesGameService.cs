using System;
using System.Collections.Generic;
using System.IO;
using TrialTask_Bees.DataSaving;
using TrialTask_Bees.Interfaces;
using TrialTask_Bees.Logging;

namespace TrialTask_Bees
{
    public class BeesGameService : IGameService
    {
        Dictionary<string, IGame> _games;

        public Dictionary<string, IGame> games
        {
            get
            {
                if (_games == null) _games = new Dictionary<string, IGame>();
                return _games;
            }

            set
            {
                _games = value;
            }
        }

        public void Add(string key, IGame game)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                if (games.ContainsKey(key))
                {
                    games.Remove(key);
                }
                games.Add(key, game);
            }
            else
            {
                Logger.Log.Error("Cannot add to the dictionary a key-pair with a null key.");
            }
        }
        
        public IGame Get(string key)
        {
            IGame result = null;
            if (!string.IsNullOrWhiteSpace(key))
            {               
                if(!games.TryGetValue(key, out result))
                    Logger.Log.Error("Cannot get a value from the dictionary with a given key.");
            }
            else
            {
                Logger.Log.Error("Cannot get a value from the dictionary with a null key.");
                
            }
            return result;
        }

        public void Remove(IGame game)
        {
            if(game != null)
            {
                if(games.ContainsValue(game))
                {
                    foreach(KeyValuePair<string, IGame> pair in games)
                    {
                        if (pair.Value == game)
                        {
                            games.Remove(pair.Key);
                        }
                    }
                }
            }
        }

        public void Remove(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                games.Remove(key);
            }
            else
            {
                Logger.Log.Error("Cannot get a value from the dictionary with a null key.");
            }
        }

        public bool Save(string key, IDataSaverController controller)
        {
            IGame game = Get(key);
            if (game != null)
            {
                try {
                    game.Save(controller);
                    return true;
                }
                catch(Exception ex) { Logger.Log.Error(ex.Message, ex); }
            }
            return false;
        }
    }
}