using UnityEngine;
using UnityEngine.InputSystem;

namespace WhiteWorld.AppMain
{
    public class PlayerMove : MonoBehaviour
    {
        private Vector2 _mMoveValue;
        private Vector2 _mVelocity = Vector2.zero;
        private Vector2 _mOldVelocity = Vector2.zero;
        [SerializeField]
        private float mMoveSpeed = 5f;
        /// <summary> 曲がるときの急さ </summary>
        [SerializeField]
        private float mSharpness = 20f;
        private CharacterController _controller;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        void FixedUpdate()
        {
            Move(_mMoveValue);
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
            _mVelocity = Vector2.Lerp(_mOldVelocity, value, mSharpness * Time.deltaTime);
            _mOldVelocity = _mVelocity;
            Vector3 velocity = new Vector3(_mVelocity.x, 0, _mVelocity.y);
            _controller.Move(velocity * mMoveSpeed * Time.deltaTime);
            if (_mVelocity.magnitude < 0.1f) return;

            transform.LookAt( transform.position + velocity, Vector3.up);
        }
    }
}
