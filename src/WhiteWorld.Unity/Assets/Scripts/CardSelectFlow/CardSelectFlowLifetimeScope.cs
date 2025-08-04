using CardSelectFlow;
using CardSelectFlow.ForDebug;
using CardSelectFlow.Interface;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CardSelectFlowLifetimeScope : LifetimeScope
{
    [SerializeField]
    private DebugFlow _flow;

    [SerializeField]
    private DebugcardNumberUser _user;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<ICardSelectFlowController, CardSelectFlowController>(Lifetime.Singleton);
        builder.Register<IAppearCardDecisionAlgorithm, RandomSelectAlgorithm>(Lifetime.Singleton);
        builder.RegisterComponentInHierarchy<CardAnimationManager>().AsImplementedInterfaces();
        builder.RegisterComponentInHierarchy<CardObjectManager>().AsImplementedInterfaces();
        builder.Register<CardObjectResetter>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

        //Debug
        builder.RegisterComponent(_flow);
        builder.RegisterComponent(_user);
    }
}
