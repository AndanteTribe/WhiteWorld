using VContainer;
using VContainer.Unity;
using AndanteTribe.Utils.Unity.VContainer;

namespace WhiteWorld.AppMain
{
    /// <summary>
    /// メッセージウィンドウUIの<see cref="LifetimeScope"/>.
    /// </summary>
    public class MessageWindowLifetimeScope : LifetimeScopeBase
    {
        /// <inheritdoc/>
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
        }
    }
}