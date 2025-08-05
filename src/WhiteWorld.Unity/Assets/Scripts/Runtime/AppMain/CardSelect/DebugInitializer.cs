using UnityEngine;
using VContainer;
using VContainer.Unity;
using WhiteWorld.Domain.LifeGame.Sequences;

namespace WhiteWorld.Presentation
{
    public class DebugInitializer : IStartable
    {
        [Inject]
        private CardSelectionSequence _sequence;

        public void Start()
        {
            Debug.Log("Initialize");
            _sequence.RunAsync(default);
        }
    }
}