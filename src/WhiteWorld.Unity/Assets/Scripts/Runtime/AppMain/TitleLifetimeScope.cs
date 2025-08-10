using AndanteTribe.Utils.Unity.VContainer;
using VContainer;
using VContainer.Unity;

namespace WhiteWorld.AppMain
{
    /// <summary>
    /// タイトルシーンの<see cref="LifetimeScope"/>.
    /// </summary>
    public class TitleLifetimeScope : LifetimeScopeBase
    {
        /// <inheritdoc/>
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
        }
    }
}