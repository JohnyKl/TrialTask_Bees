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
                if (Bees[randIndex].Type == Bee.BeeTypes.Queen)
                {
                    TotalKilled += Bees.Count;
                    Bees.Clear();
                }
                else
                {
                    TotalKilled++;
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
            Bees.Clear();
            TotalKilled = 0;
            HitCount = 0;
        }

        public void Save(IDataSaverController controller)
        {
            controller.Save(this);
        }

        /// <summary>
        /// Create a copy of the instance in a memory, save it to the file, add it to the in-memory repository.
        /// </summary>
        //public void Save(string fileName)
        //{
        //    Repo.Add(this);

        //    int counter = 1;

        //    while(File.Exists(string.Format(fileName, counter)))
        //    {
        //        counter++;
        //    }

        //    using (FileStream fSteam = File.Create(string.Format(fileName, counter)))
        //    {
        //        XmlSerializer xmlSerializer =
        //            new XmlSerializer(typeof(BeesGame));
        //        xmlSerializer.Serialize(fSteam, this);
        //    }

        //    SavedCopy = Copy();
        //}
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

        private int _id;
        private int _totalKilled;
        private int _hitCount;

        private Random _rand = new Random(DateTime.Now.Millisecond);
        private List<Bee> _bees = new List<Bee>();
        private BeesGame _savedCopy;
        
        private static int _idCounter = 0;
    }
}