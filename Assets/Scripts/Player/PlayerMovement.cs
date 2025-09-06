using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Animator _animator;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private bool _useJoystick;
        private Vector3 _movement;
        public Vector3 Movement => _movement;

        private void Update()
        {
            // если игрок двигает джойстик, то используем джойстик
            if (_joystick && (_joystick.Horizontal != 0f || _joystick.Vertical > 0f))
                _useJoystick = true;
            // если игрок нажимает клавиши, то используем клавиатуру
            else if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
                _useJoystick = false;
            
            Move();
        }
        
        private void Move()
        {
            float horizontal = _useJoystick && _joystick ? _joystick.Horizontal : Input.GetAxisRaw("Horizontal");
            float vertical = _useJoystick && _joystick ? _joystick.Vertical : Input.GetAxisRaw("Vertical");
            
            _movement = new Vector3(horizontal, vertical, 0);
            transform.position += _movement.normalized * (_moveSpeed * Time.deltaTime);
            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);
            _animator.SetFloat("Speed", _movement.sqrMagnitude);
        }
    }
}
