#if UNITY_EDITOR

using UnityEngine;

namespace WhiteWorld.Presentation.LifeGame
{
    /// <summary>
    /// Planeにギズモ表示させるためだけのクラス.
    /// </summary>
    public class PlaneGizmo : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            var size = GetComponent<MeshRenderer>().bounds.size;
            size.y = 2;

            Gizmos.color = Color.darkGreen;
            Gizmos.DrawCube(transform.position, size);
        }
    }
}
#endif