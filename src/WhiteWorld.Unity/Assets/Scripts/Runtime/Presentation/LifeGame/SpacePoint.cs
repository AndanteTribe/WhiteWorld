using UnityEngine;
using Space = WhiteWorld.Domain.Entity.Space;

namespace WhiteWorld.Presentation.LifeGame
{
    public class SpacePoint : MonoBehaviour
    {
        [SerializeField]
        private Space _space = Space.Invalid;

        public Space Space => _space;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position + new Vector3(0, 1.5f, 1.5f), 1.5f);
        }
#endif
    }
}