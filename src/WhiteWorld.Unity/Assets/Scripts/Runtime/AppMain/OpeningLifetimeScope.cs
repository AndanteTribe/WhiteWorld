using AndanteTribe.Utils.Unity.VContainer;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Domain;
using WhiteWorld.Presentation;

namespace WhiteWorld.AppMain
{
    /// <summary>
    /// オープニングシーンの<see cref="LifetimeScope"/>.
    /// </summary>
    public class OpeningLifetimeScope : LifetimeScopeBase
    {
        [SerializeField] private GameObject _tvPrefab;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<OpeningCreator>().WithParameter(_tvPrefab);
        }
    }
}