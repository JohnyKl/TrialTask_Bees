using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrialTask_Bees.Models.Interfaces;

namespace TrialTask_Bees.Models.BeesGame
{
    [Serializable]
    public class BeeGameEntityInfo : IGameEntityObjectInfo
    {
        private int _number;
        private IGameEntityParameter _parameter;
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

        public IGameEntityParameter Parameter
        {
            get
            {
                if (_parameter == null)
                {
                    throw new ArgumentException("Parameter not initialized");
                }
                return _parameter;
            }

            set
            {
                _parameter = value;
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
    }
}