using AndanteTribe.Utils.Unity.VContainer;
using VContainer;
using WhiteWorld.Domain;
using WhiteWorld.Domain.LifeGame.SpaceActions;
using WhiteWorld.Presentation;

namespace WhiteWorld.AppMain
{
    public sealed class TextAnimationLifetimeScope : LifetimeScopeBase
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

        }
    }
}