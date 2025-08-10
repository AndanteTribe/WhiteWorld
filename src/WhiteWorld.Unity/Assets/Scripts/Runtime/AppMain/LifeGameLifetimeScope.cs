using AndanteTribe.Utils.Unity.VContainer;
using VContainer;
using WhiteWorld.Domain.LifeGame;
using WhiteWorld.Domain.LifeGame.Sequences;
using WhiteWorld.Domain.LifeGame.SpaceActions;

namespace WhiteWorld.AppMain
{
    public class LifeGameLifetimeScope : LifetimeScopeBase
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            // シーケンス
            builder.Register<LifeGameMainSequence>(Lifetime.Singleton);
            builder.Register<CardSelectionSequence>(Lifetime.Singleton).As<ICardSelectionSequence>();
            builder.Register<SpaceMoveSequence>(Lifetime.Singleton);
            builder.Register<SpaceActionSequence>(Lifetime.Singleton);

            // スペースアクション
            builder.Register<ISpaceAction, StartSpace>(Lifetime.Singleton);
            builder.Register<ISpaceAction, GoalSpace>(Lifetime.Singleton);
            builder.Register<ISpaceAction, ProceedSpace>(Lifetime.Singleton);
            builder.Register<ISpaceAction, ReturnSpace>(Lifetime.Singleton);
            builder.Register<ISpaceAction, TelevisionSpace>(Lifetime.Singleton);
            builder.Register<ISpaceAction, MemoryPieceSpace>(Lifetime.Singleton);
            builder.Register<ISpaceAction, FlavorSpace>(Lifetime.Singleton);

            builder.RegisterEntryPoints(static builder =>
            {
                builder.Add<LifeGame>(true);
            });
        }
    }
}