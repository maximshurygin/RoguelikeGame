using System.Collections;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float freezeTimer;
        [SerializeField] private Animator animator;
        private Vector3 _direction;
        private PlayerMovement _playerMovement;
        private WaitForSeconds _checkTime = new WaitForSeconds(3f);
        
        [Inject] private void Construct(PlayerMovement playerMovement) => _playerMovement = playerMovement;

        private void OnEnable()
        {
            StartCoroutine(CheckDistanceToHide());
        }
        
        
        private void Update() => Move();
        private void Move()
        {
            _direction = (_playerMovement.transform.position - transform.position).normalized;
            transform.position += _direction * (moveSpeed * Time.deltaTime);
            animator.SetFloat("Horizontal", _direction.x);
            animator.SetFloat("Vertical", _direction.y);
        }

        private IEnumerator CheckDistanceToHide()
        {
            while (enabled)
            {
                float distance = Vector3.Distance(transform.position, _playerMovement.transform.position);
                if (distance > 20f)
                {
                    gameObject.SetActive(false);
                }
                yield return _checkTime;;
            }
        }
    }
}