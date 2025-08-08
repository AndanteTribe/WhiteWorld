using AndanteTribe.Utils.Unity.VContainer;
using UnityEngine;
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
        [SerializeField] private GameObject _tvPrefab;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISceneController, DefaultSceneController>(Lifetime.Singleton);

#if ENABLE_DEBUGTOOLKIT
            builder.RegisterEntryPoint<DebugViewer>().WithParameter(_tvPrefab);
#endif
        }
    }
}
