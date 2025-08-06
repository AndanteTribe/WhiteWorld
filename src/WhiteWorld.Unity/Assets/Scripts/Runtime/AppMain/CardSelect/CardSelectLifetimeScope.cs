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

            //TODO 本番ではこっちを使う
            // builder.Register<ICardSelectionSequence,CardSelectionSequence>(Lifetime.Singleton);

            //Debug
            builder.Register<IAppearCardDecisionAlgorithm, RandomSelectAlgorithm>(Lifetime.Singleton);
        }
    }
}

