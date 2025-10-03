using UnityEngine;

namespace GameCore.ParticleSystem
{
    public class PlayerUpgradeParticleSpawner: ParticleSpawner
    {
        public override void Spawn()
        {
            var particleObject = _objectPool.GetFromPool();
            if (particleObject != null)
            {
                particleObject.transform.SetParent(transform, true);

                Vector3 offset = new Vector3(0f, -1f, 0.5f);
                particleObject.transform.localPosition = offset;
            }
        }
    }
}