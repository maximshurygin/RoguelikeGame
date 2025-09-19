using System;
using UnityEngine;

namespace Player.Weapon
{
    [Serializable]
    public class WeaponStats
    {
        [SerializeField] private float speed, damage, range, timeBetweenAttack, duration;

        public float Speed => speed;
        public float Damage => damage;
        public float Range => range;
        public float TimeBetweenAttack => timeBetweenAttack;
        public float Duration => duration;
    }
}