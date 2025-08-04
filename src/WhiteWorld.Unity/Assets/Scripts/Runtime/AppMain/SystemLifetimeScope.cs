using AndanteTribe.Utils.Unity.VContainer;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Domain;
using WhiteWorld.Presentation;

namespace WhiteWorld.AppMain
{
    /// <summary>
    /// Systemシーンの<see cref="LifetimeScope"/>.
    /// </summary>
    public sealed class SystemLifetimeScope : LifetimeScopeBase
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<DebugViewer>();
            builder.Register<ISceneController, DefaultSceneController>(Lifetime.Singleton);
        }
    }
}
