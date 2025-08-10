using Unity.Cinemachine;
using UnityEngine;
using WhiteWorld.Domain;

namespace WhiteWorld.Presentation
{
    public class PlayerTVInteraction : MonoBehaviour, ISwitchToPlayerCamera
    {
        private static readonly int s_color = Shader.PropertyToID("_Color");
        [SerializeField] private Renderer _playerRenderer;
        [SerializeField] private PlayerMove _player;
        private bool _isInteracting = false;
        private ITVController? _tvController;

        public void SwitchToPlayerCamera()
        {
            _playerRenderer.enabled = true;
            _tvController?.EndTVAnimation();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITVController? tvController) && !_isInteracting)
            {
                _isInteracting = true;
                _tvController = tvController;
                _tvController?.StartTVAnimation();
                _player.CanMove = false;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ITVController? tvController) && _isInteracting)
            {
                SwitchToPlayerCamera();
                _isInteracting = false;
                _player.CanMove = true;
            }
        }
    }
}