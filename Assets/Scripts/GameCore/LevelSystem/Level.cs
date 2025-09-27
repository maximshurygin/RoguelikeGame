using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace GameCore.LevelSystem
{
    [Serializable]
    public class Level: IActivate
    {
        [SerializeField] private List<EnemySpawner> _enemySpawner = new List<EnemySpawner>();

        public void Activate()
        {
            for (int i = 0; i < _enemySpawner.Count; i++)
            {
                _enemySpawner[i].Activate();
            }
        }

        public void Deactivate()
        {
            for (int i = 0; i < _enemySpawner.Count; i++)
            {
                _enemySpawner[i].Deactivate();
            }
        }
    }
}