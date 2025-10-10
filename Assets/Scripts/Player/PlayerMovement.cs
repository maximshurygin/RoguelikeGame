using GameCore.Pause;
using Menu.Shop;
using UnityEngine;
using Zenject;

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
        private Vector3 _movement;
        private Rigidbody2D _rigidbody;
        private GamePause _gamePause;
        private float _initialSpeed;
        private UpgradeLoader _upgradeLoader;

        [Inject]
        private void Construct(GamePause gamePause, UpgradeLoader upgradeLoader)
        {
            _gamePause = gamePause;
            _upgradeLoader = upgradeLoader;
        }
        
        // public Vector3 Movement => _movement;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            moveSpeed = _upgradeLoader.SpeedCurrentLevel.Value;
            _initialSpeed = moveSpeed;
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
        }

        public void UpgradeSpeed()
        {
            _initialSpeed += 0.5f;
            moveSpeed = _initialSpeed;
        }
        
        private void Move()
        {
            moveSpeed = _gamePause.IsStopped ? 0f : _initialSpeed;
            float horizontal = _device == Device.Joystick&& joystick ? joystick.Horizontal : Input.GetAxisRaw("Horizontal");
            float vertical = _device == Device.Joystick&& joystick ? joystick.Vertical : Input.GetAxisRaw("Vertical");
            
            _movement = new Vector3(horizontal, vertical, 0).normalized;
            _rigidbody.velocity = _movement * moveSpeed;
            animator.SetFloat("Horizontal", _movement.x);
            animator.SetFloat("Vertical", _movement.y);
            animator.SetFloat("Speed", _movement.sqrMagnitude);
        }
    }
}
