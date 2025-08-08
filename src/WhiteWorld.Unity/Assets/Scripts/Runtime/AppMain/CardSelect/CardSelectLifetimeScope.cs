using AndanteTribe.Utils.Unity.VContainer;
using CardSelectFlow.Interface;
using VContainer;
using WhiteWorld.Presentation;

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

