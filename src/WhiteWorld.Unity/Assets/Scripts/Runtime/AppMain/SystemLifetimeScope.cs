using AndanteTribe.Utils.Unity.VContainer;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Data;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity;
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
            builder
                .Register<IMasterDataRepository<DummyModel>, MasterDataRepository<DummyModel>>(Lifetime.Singleton)
                .WithParameter("binaryPath", "DummyText")
                .WithParameter("tableName", "DummyData");

            builder
                .Register<IMasterDataRepository<KeywordModel>, KeywordModelRepository>(Lifetime.Singleton)
                .WithParameter("binaryPath", "Keyword")
                .WithParameter("tableName", "KeywordData");

            builder
                .Register<IMasterDataRepository<MessageModel>, MasterDataRepository<MessageModel>>(Lifetime.Singleton)
                .WithParameter("binaryPath", "Message")
                .WithParameter("tableName", "MessageData");

            builder.RegisterEntryPoints(static builder =>
            {
                builder.RegisterEnqueue<SystemInitializer>(true).AsSelf();
            });
        }
    }
}