using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WhiteWorld.AppMain
{
    /// <summary> InputActionにメソッドを割り当てる </summary>
    public class InputActionAttach : MonoBehaviour
    {
        public WhiteWorldActions MWhiteWorldActions { get; private set; }
        [SerializeField]
        private PlayerMove _mPlayerMove;
        void Awake()
        {
            MWhiteWorldActions = new WhiteWorldActions();
            MWhiteWorldActions.Player.Enable();
            MWhiteWorldActions.Player.Move.performed += context => _mPlayerMove.ReadMoveValue(context);
            MWhiteWorldActions.Player.Move.canceled += context => _mPlayerMove.ReadMoveValue(context);
        }
    }
}
