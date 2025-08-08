using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace WhiteWorld.Presentation
{
    public class OpeningCreator : IStartable
    {
        private readonly IObjectResolver _resolver;
        private readonly GameObject _tvPrefab;

        public OpeningCreator(IObjectResolver resolver, GameObject tvPrefab)
        {
            _resolver = resolver;
            _tvPrefab = tvPrefab;
        }

        public void Start()
        {
            _resolver.Instantiate(_tvPrefab);
        }
    }
}