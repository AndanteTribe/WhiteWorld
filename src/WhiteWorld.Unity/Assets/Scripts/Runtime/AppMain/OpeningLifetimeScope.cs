using AndanteTribe.Utils.Unity.VContainer;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Domain.Opening;
using WhiteWorld.Presentation;

namespace WhiteWorld.AppMain
{
    /// <summary>
    /// オープニングシーンの<see cref="LifetimeScope"/>.
    /// </summary>
    public class OpeningLifetimeScope : LifetimeScopeBase
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            using (builder.RegisterEntryPoints(out var ep))
            {
                ep.RegisterEnqueue<Opening>(true).AsSelf();
            }
        }
    }
}