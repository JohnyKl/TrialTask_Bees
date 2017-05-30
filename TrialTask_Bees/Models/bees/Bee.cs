using System;
using System.Xml.Serialization;
using TrialTask_Bees.Interfaces;
using TrialTask_Bees.Models.bees;
using TrialTask_Bees.Models.Interfaces;

namespace TrialTask_Bees
{
    [Serializable]
    [XmlInclude(typeof(QueenBee))]
    [XmlInclude(typeof(WorkerBee))]
    [XmlInclude(typeof(DroneBee))]
    public abstract class Bee : INumerable, IDisposable
    {
        public Bee() : this(0) { }

        public Bee(int id)
        {
            Id = id;
        }

        public string Name
        {
            get { return _name; }

            protected set { _name = string.Format("{0} Bee{1}", value, Id); }
        }
        
        public bool IsAlive
        {
            get { return !(Parameters.Health < BeeParameter.MinimalHealth); }
        }
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public IGameEntityParameter Parameters
        {
            get
            {
                return _parameters;
            }

            set
            {
                _parameters = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Parameters.Health);
        }
        
        public void Hit()
        {
            Parameters.IncreaseHealth();
        }

        public abstract void Dispose();

        private IGameEntityParameter _parameters;
        private string _name;
        private int _id;
    }
}