using UnityEngine;

namespace WhiteWorld.Presentation
{

    public class CardSlotHolder : MonoBehaviour
    {
        [SerializeField]
        private CardSlot _slot;

        public CardSlot CardSlot => _slot;
    }
}