using AndanteTribe.Utils.Unity.VContainer;
using VContainer;
using WhiteWorld.Domain.LifeGame;

namespace WhiteWorld.AppMain
{
    public class LifeGameLifetimeScope : LifetimeScopeBase
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<LifeGameMainSequence>(Lifetime.Singleton);
            builder.RegisterEntryPoints(static builder =>
            {
                builder.Add<LifeGame>(true);
            });
        }
    }
}