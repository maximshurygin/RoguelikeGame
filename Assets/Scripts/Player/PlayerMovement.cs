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
        [SerializeField] private float moveSpeed;
        [SerializeField] private Animator animator;
        [SerializeField] private Joystick joystick;
        private Device _device;
        private bool _isInteracting;
        private Vector3 _movement;
        private Rigidbody2D _rigidbody;
        
        public Vector3 Movement => _movement;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            // если игрок двигает джойстик, то используем джойстик
            if (joystick && (joystick.Horizontal != 0f || joystick.Vertical > 0f))
                _device = Device.Joystick;
            // если игрок нажимает клавиши, то используем клавиатуру
            else if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
                _device = Device.Keyboard;
            
            Move();
            Interact();
        }

        public void UpgradeSpeed()
        {
            moveSpeed += 0.3f;
        }
        
        private void Move()
        {
            float horizontal = _device == Device.Joystick&& joystick ? joystick.Horizontal : Input.GetAxisRaw("Horizontal");
            float vertical = _device == Device.Joystick&& joystick ? joystick.Vertical : Input.GetAxisRaw("Vertical");
            
            _movement = new Vector3(horizontal, vertical, 0).normalized;
            _rigidbody.velocity = _movement * moveSpeed;
            animator.SetFloat("Horizontal", _movement.x);
            animator.SetFloat("Vertical", _movement.y);
            animator.SetFloat("Speed", _movement.sqrMagnitude);
            animator.SetBool("IsInteracting", _isInteracting);
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
