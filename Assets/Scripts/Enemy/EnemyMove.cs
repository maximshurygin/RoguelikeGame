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
        private Coroutine _slowdownCoroutine;
        private float moveSpeedTemp;
        

        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }

        private void Start()
        {
            moveSpeedTemp = moveSpeed;
        }

        private void OnEnable()
        {
            StartCoroutine(CheckDistanceToHide());
            if (moveSpeedTemp != 0)
            {
                moveSpeed = moveSpeedTemp;
            }
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

        public void SlowDown(float slowdownRate, WaitForSeconds duration)
        {
            if (!gameObject.activeInHierarchy) return;
            StartCoroutine(ApplySlowdown(slowdownRate, duration));
        }
        
        private IEnumerator ApplySlowdown(float slowdownRate, WaitForSeconds duration)
        {
            if (_slowdownCoroutine != null)
            {
                StopCoroutine(_slowdownCoroutine);
            }
            moveSpeed /= slowdownRate;
            yield return duration;
            moveSpeed = moveSpeedTemp;
            _slowdownCoroutine = null;
        }
    }
}