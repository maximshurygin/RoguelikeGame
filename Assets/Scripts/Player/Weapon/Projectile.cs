using System.Collections;
using Enemy;
using GameCore.GameObjectPool;
using UnityEngine;

namespace Player.Weapon
{
    public abstract class Projectile : MonoBehaviour
    {
        protected WaitForSeconds Timer;
        protected float Damage;

        protected virtual void OnEnable()
        {
            StartCoroutine(TimerToHide());
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyHealth enemy))
            {
                float damage = Random.Range(Damage / 1.5f, Damage * 1.5f);
                if (damage < 1)
                {
                    damage = 1;
                }
                enemy.TakeDamage(damage);
            }
        }
        private IEnumerator TimerToHide()
        {
            yield return Timer;
            gameObject.SetActive(false);
        }
        
    }
}