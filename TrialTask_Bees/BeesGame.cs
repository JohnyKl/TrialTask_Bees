using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using TrialTask_Bees.DataSaving;
using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees
{
    [Serializable]
    public class BeesGame : INumerable, IMemorySaveable<BeesGame>
    {
        public BeesGame()
        {
            _idCounter++;
            Id = _idCounter;
        }

        public int GetTotalKilled()
        {
            return TotalKilled;
        }


        /// <summary>
        /// Create a new bee of a given type with specified health points and hit points
        /// </summary>
        public void CreateBee<T>(int count, int health, int hitPoints) where T : Bee
        {
            while (count > 0)
            {                
                Bee _newBee = bees.BeesFactory.CreateBee<T>(count, health, hitPoints);
                      
                if(_newBee is QueenBee)
                {
                    Queen = (QueenBee)_newBee;
                }
                          
                Bees.Add(_newBee);
                count--;
            }
        }

        public List<Bee> Bees
        {
            get
            {
                return _bees;
            }
        }

        [XmlAttribute]
        public int TotalKilled
        {
            get
            {
                return _totalKilled;
            }

            set
            {
                _totalKilled = value;
            }
        }

        [XmlAttribute]
        public int HitCount
        {
            get
            {
                return _hitCount;
            }

            set
            {
                _hitCount = value;
            }
        }

        [XmlIgnore]
        public BeesGame SavedCopy
        {
            get
            {
                return _savedCopy;
            }

            set
            {
                _savedCopy = value;
            }
        }

        [XmlAttribute]
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
        
        public bool IsGameOver()
        {
            return Bees.Count == 0;
        }

        /// <summary>
        /// Hit a random bee. If the Queen bee was killed, all others bees die too.
        /// </summary>
        public void HitRandomBee()
        {
            HitCount++;

            int randIndex = _rand.Next(0, Bees.Count);

            Bees[randIndex].Hit();

            if (!Bees[randIndex].IsAlive)
            {
                if(Queen == Bees[randIndex])
                {
                    //TotalKilled += Bees.Count;
                    Restart();
                }
                else
                {
                    TotalKilled++;

                    Bees[randIndex].Dispose();
                    Bees.RemoveAt(randIndex);
                }
            }
        }


        /// <summary>
        /// Return a string info about alive bees
        /// </summary>
        public string AliveBeesToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Bee bee in Bees)
            {
                sb.Append(bee);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public void Restart()
        {
            foreach (Bee bee in Bees)
                bee.Dispose();
            Bees.Clear();
            TotalKilled = 0;
            HitCount = 0;
        }

        public void Save(IDataSaverController controller)
        {
            controller.Save(this);
        }
                
        public BeesGame Copy()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (BeesGame)formatter.Deserialize(stream);
            }
        }

        private QueenBee Queen
        {
            get { return _queen; }
            set
            {
                if (_queen != null) throw new MemberAccessException("Cannot add the new QueenBee to the game. Only one QueenBee is allowed.");
                _queen = value;
            }
        }

        private int _id;
        private int _totalKilled;
        private int _hitCount;

        private Random _rand = new Random(DateTime.Now.Millisecond);
        private List<Bee> _bees = new List<Bee>();
        private QueenBee _queen;
        private BeesGame _savedCopy;
        
        private static int _idCounter = 0;
    }
}