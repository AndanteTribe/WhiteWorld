using AndanteTribe.Utils.Unity.VContainer;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Data.Runtime.Data;
using WhiteWorld.Domain;
using WhiteWorld.Domain.Entity.Runtime.Domain.Entity;
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

            builder
                .Register<IMasterDataRepository<DummyModel>, MasterDataRepository<DummyModel>>(Lifetime.Singleton)
                .WithParameter("binaryPath","DummyText")
                .WithParameter("tableName","DummyData");

            builder
                .Register<IMasterDataRepository<KeywordModel>, KeywordModelRepository>(Lifetime.Singleton)
                .WithParameter("binaryPath", "Keyword")
                .WithParameter("tableName", "KeywordData");
        }
    }
}
