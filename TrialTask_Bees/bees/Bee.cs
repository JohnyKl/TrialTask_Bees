using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace TrialTask_Bees
{
    [Serializable]
    [XmlInclude(typeof(QueenBee))]
    [XmlInclude(typeof(WorkerBee))]
    [XmlInclude(typeof(DroneBee))]
    public abstract class Bee
    {
        public enum BeeTypes
        {
            DroneBee, WorkerBee, QueenBee
        }
        
        public string Name
        {
            get { return _name; }
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
        private BeeTypes _type;
    }
}