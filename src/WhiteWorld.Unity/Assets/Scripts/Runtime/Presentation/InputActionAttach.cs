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
        private PlayerMove mPlayerMove;
        void Awake()
        {
            MWhiteWorldActions = new WhiteWorldActions();
            MWhiteWorldActions.Player.Enable();
            MWhiteWorldActions.Player.Move.performed += context => mPlayerMove.ReadMoveValue(context);
            MWhiteWorldActions.Player.Move.canceled += context => mPlayerMove.ReadMoveValue(context);
        }
    }
}
