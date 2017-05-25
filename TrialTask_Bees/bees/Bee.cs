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
    public abstract class Bee : INumerable
    {
        public enum BeeTypes
        {
            Drone, Worker, Queen
        }

        public Bee() : this(0) { }

        public Bee(int id)
        {
            Id = id;
        }

        public string Name
        {
            get
            {
                if(string.IsNullOrWhiteSpace(_name))
                {
                    _name = string.Format("{0} Bee{1}", Enum.GetName(typeof(BeeTypes), Type), Id); 
                }

                return _name;
            }
            set { _name = value; }
        }
        
        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                if (value > 0)
                {
                    _health = value;
                }
                else
                {
                    _health = 1;
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
                if (value > 0)
                {
                    _hitPoints = value;
                }
                else
                {
                    _hitPoints = 1;
                }
            }
        }
        
        public bool IsAlive
        {
            get { return Health > 0; }
        }
        
        public BeeTypes Type
        {
            get { return _type; }
            set { _type = value; }
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

        private string _name;
        private int _health;
        private int _hitPoints;
        private int _id;
        private BeeTypes _type;
    }
}