using CardSelectFlow.Interface;
using R3;
using UnityEngine;
using VContainer;

namespace CardSelectFlow.ForDebug
{
    /// <summary>
    /// カードの番号を使う人
    /// </summary>
    public class DebugcardNumberUser : MonoBehaviour
    {
        [Inject]
        private ICardSelectFlowController _controller;

        private void Start()
        {
            _controller.OnCardSelected.Subscribe(val => Debug.Log($"Number is {val.Number}"));
        }
    }
}