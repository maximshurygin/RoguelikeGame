using UnityEngine;

namespace Player
{
    // enum для выбора способа управления персонажем
    enum Device
    {
        Keyboard,
        Joystick
    }
    
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Animator _animator;
        [SerializeField] private Joystick _joystick;
        private Device _device;
        private bool _isInteracting;
        private Vector3 _movement;

        private void Update()
        {
            // если игрок двигает джойстик, то используем джойстик
            if (_joystick && (_joystick.Horizontal != 0f || _joystick.Vertical > 0f))
                _device = Device.Joystick;
            // если игрок нажимает клавиши, то используем клавиатуру
            else if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
                _device = Device.Keyboard;
            
            Move();
            Interact();
        }
        
        private void Move()
        {
            float horizontal = _device == Device.Joystick&& _joystick ? _joystick.Horizontal : Input.GetAxisRaw("Horizontal");
            float vertical = _device == Device.Joystick&& _joystick ? _joystick.Vertical : Input.GetAxisRaw("Vertical");
            
            _movement = new Vector3(horizontal, vertical, 0);
            transform.position += _movement.normalized * (_moveSpeed * Time.deltaTime);
            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);
            _animator.SetFloat("Speed", _movement.sqrMagnitude);
            _animator.SetBool("IsInteracting", _isInteracting);
        }

        private void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isInteracting = true;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                _isInteracting = false;
            }
        }
    }
}
