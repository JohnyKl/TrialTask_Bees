using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees
{
    [Serializable]
    [XmlInclude(typeof(QueenBee))]
    [XmlInclude(typeof(WorkerBee))]
    [XmlInclude(typeof(DroneBee))]
    public abstract class Bee : INumerable, IDisposable
    {        
        public static readonly int MinimalHealth = 1;
        public static readonly int MinimalHitPoints = 1;
                
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
        
        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                if (value > MinimalHealth)
                {
                    _health = value;
                }
                else
                {
                    _health = MinimalHealth;
                }
            }
        }

        public int HitPoints
        {
            get
            {
                return _hitPoints;
            }

            set
            {
                if (value > MinimalHitPoints)
                {
                    _hitPoints = value;
                }
                else
                {
                    _hitPoints = MinimalHitPoints;
                }
            }
        }
        
        public bool IsAlive
        {
            get { return !(Health < MinimalHealth); }
        }
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Health);
        }
        
        public void Hit()
        {
            _health -= _hitPoints;
        }

        public abstract void Dispose();

        private string _name;
        private int _health;
        private int _hitPoints;
        private int _id;
    }
}