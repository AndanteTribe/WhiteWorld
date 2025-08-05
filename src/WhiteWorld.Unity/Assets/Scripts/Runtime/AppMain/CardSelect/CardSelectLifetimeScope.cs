using AndanteTribe.Utils.Unity.VContainer;
using CardSelectFlow;
using CardSelectFlow.Interface;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Domain.LifeGame.Sequences;

namespace WhiteWorld.AppMain
{
    public class CardSelectLifetimeScope : LifetimeScopeBase
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<CardSelectionSequence>(Lifetime.Singleton);

            Debug.Log("前");
            //Debug
            builder.Register<IAppearCardDecisionAlgorithm, RandomSelectAlgorithm>(Lifetime.Singleton);
            Debug.Log("後");


            builder.RegisterDisposeCallback(container =>
            {
                Debug.Log("Resolve");
                var sequence = container.Resolve<CardSelectionSequence>();
                var alg = container.Resolve<IAppearCardDecisionAlgorithm>();
            });
        }
    }
}

