using System;
using TrialTask_Bees.Interfaces;

namespace TrialTask_Bees
{
    [Serializable]
    public class BeeGameEntityInfo : IGameEntityObjectInfo
    {
        private int _number;
        private int _health;
        private int _hitPoint;
        private Type _type;
        
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;

                if (_number < 0) _number = 0;
            }
        }


        public Type Type
        {
            get
            {
                return _type;
            }

            set
            {
                if (value.IsSubclassOf(typeof(Bee)))
                {
                    _type = value;
                }
                else
                {
                    throw new ArgumentException("Given type is not a subclass of a Bee class");
                }
            }
        }

        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                _health = value;
            }
        }

        public int HitPoint
        {
            get
            {
                return _hitPoint;
            }

            set
            {
                _hitPoint = value;
            }
        }
    }
}