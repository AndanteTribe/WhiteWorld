using System.IO;
using AndanteTribe.Utils.Unity.VContainer;
using MasterMemory;
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
        [SerializeField]
        private GameObject _tvPrefab;

        [SerializeField]
        private AudioController _audioController;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISceneController, DefaultSceneController>(Lifetime.Singleton);

            builder.RegisterComponent(_audioController);

#if ENABLE_DEBUGTOOLKIT
            builder.RegisterEntryPoint<DebugViewer>().WithParameter(_tvPrefab);
#endif

            var data = Resources.Load("MasterData") as TextAsset;
            var memoryDatabase = new MemoryDatabase(data?.bytes?? throw new FileNotFoundException("MasterDataが見つかりませんでした。"));
            builder.RegisterInstance(memoryDatabase.KeywordModelTable);
            builder.RegisterInstance(memoryDatabase.DummyModelTable);
            builder.RegisterInstance(memoryDatabase.MessageModelTable);

            using (builder.RegisterEntryPoints(out var ep))
            {
                ep.RegisterEnqueue<SystemInitializer>(true).AsSelf();
            }
        }
    }
}