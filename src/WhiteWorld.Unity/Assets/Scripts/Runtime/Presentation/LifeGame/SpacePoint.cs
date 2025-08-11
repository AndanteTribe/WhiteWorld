using UnityEngine;
using WhiteWorld.Domain;
using Space = WhiteWorld.Domain.Entity.Space;

namespace WhiteWorld.Presentation.LifeGame
{
    public class SpacePoint : MonoBehaviour
    {
        [SerializeField]
        private Space _space = Space.Invalid;
        [SerializeField]
        private TVController _tvController;

        public Space Space => _space;

        public ITVController TVController => _tvController;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position + new Vector3(0, 1.5f, 1.5f), 1.5f);
        }
#endif
    }
}