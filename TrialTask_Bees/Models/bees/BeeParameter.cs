using System;
using TrialTask_Bees.Models.Interfaces;

namespace TrialTask_Bees.Models.bees
{
    [Serializable]
    public class BeeParameter : IGameEntityParameter
    {
        public static readonly int MinimalHealth = 1;
        public static readonly int MinimalHitPoints = 1;

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

        public void IncreaseHealth()
        {
            _health -= _hitPoints;
        }

        public IGameEntityParameter Copy()
        {
            return new BeeParameter() { Health = _health, HitPoints = _hitPoints };
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

        private int _health;
        private int _hitPoints;
    }
}