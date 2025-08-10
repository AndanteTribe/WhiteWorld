using UnityEngine;
using UnityEngine.InputSystem;
using WhiteWorld.Domain;

namespace WhiteWorld.Presentation
{
    // FIXME: 命名規則大違反スクリプト
    public class PlayerMove : MonoBehaviour, IPlayerControl
    {
        private Vector2 _mMoveValue;
        private Vector2 _mVelocity = Vector2.zero;
        private Vector2 _mOldVelocity = Vector2.zero;
        [SerializeField]
        private float _mMoveSpeed = 5f;
        /// <summary> 曲がるときの急さ </summary>
        [SerializeField]
        private float _mSharpness = 20f;
        private CharacterController _controller;

        public bool CanMove { get; set; } = true;

        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        void FixedUpdate()
        {
            if (CanMove)
            {
                Move(_mMoveValue);
            }
        }

        public void ReadMoveValue(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _mMoveValue =  context.ReadValue<Vector2>();
            }
            else if (context.canceled)
            {
                _mMoveValue = Vector2.zero;
            }
        }

        private void Move(Vector2 value)
        {
            _mVelocity = Vector2.Lerp(_mOldVelocity, value, _mSharpness * Time.fixedDeltaTime);
            _mOldVelocity = _mVelocity;
            Vector3 velocity = new Vector3(_mVelocity.x, 0, _mVelocity.y);
            _controller.Move(velocity * _mMoveSpeed * Time.fixedDeltaTime);
            if (_mVelocity.magnitude < 0.1f) return;

            transform.LookAt( transform.position + velocity, Vector3.up);
        }
    }
}
