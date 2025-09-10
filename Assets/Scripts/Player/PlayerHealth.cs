using System.Collections;
using GameCore.Health;
using UnityEngine;

namespace Player
{
    public class PlayerHealth: ObjectHealth
    {
        private WaitForSeconds _regenerationInterval = new WaitForSeconds(5f);
        private float _regenerationValue = 1f;
        public override void TakeDamage(float value)
        {
            base.TakeDamage(value);
            if (CurrentHealth <= 0)
            {
                Debug.Log("Игрок умер");
            }
        }

        private IEnumerator RegenerateHealth()
        {
            while (enabled)
            {
                yield return _regenerationInterval;
                TakeHeal(_regenerationValue);
            }
        }

        private void Start()
        {
            StartCoroutine(RegenerateHealth());
        }
    }
}