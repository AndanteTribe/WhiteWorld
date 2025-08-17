using UnityEngine;
using WhiteWorld.Domain;

namespace WhiteWorld.Presentation
{
    public class PlayerTVInteraction : MonoBehaviour
    {
        [SerializeField] private Renderer _playerRenderer;
        [SerializeField] private PlayerMove _player;
        private bool _isInteracting = false;
        private ITVController _tvController;

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITVController tvController) && !_isInteracting)
            {
                _isInteracting = true;
                _tvController = tvController;
                _tvController?.StartTVAnimation();
                _player.CanMove = false;
                _playerRenderer.enabled = false;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ITVController tvController) && _isInteracting)
            {
                _isInteracting = false;
                _player.CanMove = true;
                _playerRenderer.enabled = true;
            }
        }
    }
}