using System;
using CardSelectFlow.Interface;
using UnityEngine;
using VContainer;
using R3;

public class DebugFlow : MonoBehaviour
{
    [Inject]
    private ICardSelectFlowController _controller;

    private void Start()
    {
        _controller.OnEndCardSelectFlow.Subscribe(_ => gameObject.SetActive(true)).AddTo(this);
    }

    public void GoFlow()
    {
        _controller.Flow();
    }
}
