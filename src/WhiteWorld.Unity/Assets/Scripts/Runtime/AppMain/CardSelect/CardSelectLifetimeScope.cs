using AndanteTribe.Utils.Unity.VContainer;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Domain;
using WhiteWorld.Domain.LifeGame.Sequences;
using WhiteWorld.Presentation;

namespace WhiteWorld.AppMain
{
    public class CardSelectLifetimeScope : LifetimeScopeBase
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            //TODO 本番ではこっちを使う
            //builder.Register<ICardSelectionSequence, CardSelectionSequence>(Lifetime.Singleton);
            builder.Register<IAppearCardDecisionAlgorithm, RandomSelectAlgorithm>(Lifetime.Singleton);
        }
    }
}

