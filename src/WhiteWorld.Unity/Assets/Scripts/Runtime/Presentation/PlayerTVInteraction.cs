using Unity.Cinemachine;
using UnityEngine;
using WhiteWorld.Domain;

namespace WhiteWorld.Presentation
{
    public class PlayerTVInteraction : MonoBehaviour, ISwitchToPlayerCamera
    {
        [SerializeField] private Renderer _playerRenderer;
        [SerializeField] private CinemachineCamera _playerCamera;
        private bool _isInteracting = false;
        private ITVController? _tvController;

        public void SwitchToPlayerCamera()
        {
            _playerRenderer.enabled = true;
            _tvController?.EndTVAnimation();
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("Entered TV Controller");
            if (other.TryGetComponent(out ITVController? tvController) && !_isInteracting)
            {
                Debug.Log("Entered TV Controller");
                _isInteracting = true;
                _tvController = tvController;
                _tvController?.StartTVAnimation();
                _playerRenderer.enabled = false;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ITVController? tvController) && _isInteracting)
            {
                _isInteracting = false;
                _playerRenderer.enabled = true;
            }
        }
    }
}