using Menu;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField] private float _damage;
        private GameDifficulty _gameDifficulty;
        private float _damageMultiplier = 1f;

        [Inject]
        private void Construct(GameDifficulty gameDifficulty)
        {
            _gameDifficulty = gameDifficulty;
        }

        private void Start()
        {
            _damageMultiplier = _gameDifficulty.Difficulty == DifficultyEnum.Normal ? 1f : 2f;
            _damage *= _damageMultiplier;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth player))
            {
                player.TakeDamage(_damage);
                gameObject.SetActive(false);
            }
        }
    }
}