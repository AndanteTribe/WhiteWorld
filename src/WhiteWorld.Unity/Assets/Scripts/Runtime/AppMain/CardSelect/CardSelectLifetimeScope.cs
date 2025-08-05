using AndanteTribe.Utils.Unity.VContainer;
using CardSelectFlow;
using CardSelectFlow.Interface;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Domain.LifeGame.Sequences;
using WhiteWorld.Presentation;

namespace WhiteWorld.AppMain
{
    public class CardSelectLifetimeScope : LifetimeScopeBase
    {
        [SerializeField]
        private CardAnimationManager _animation;
        [SerializeField]
        private CardObjectManager _manager;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<CardSelectionSequence>(Lifetime.Singleton);
            builder.RegisterComponent(_animation).AsImplementedInterfaces();
            builder.RegisterComponent(_manager).AsImplementedInterfaces();

            //Debug
            builder.RegisterEntryPoint<DebugInitializer>();
            builder.Register<IAppearCardDecisionAlgorithm, RandomSelectAlgorithm>(Lifetime.Singleton);
        }
    }
}

