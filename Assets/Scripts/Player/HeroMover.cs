using Infrastructure.Service;
using UnityEngine;

namespace Player
{
    public class HeroMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private CharacterController _characterController;

        private IInputService _inputService;
        private float _epsilon = 0.001f;
        private Vector3 _currentMovement;
        private bool _isMovedPressed;
        private Camera _camera;

        private void Start() => _camera = Camera.main;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            OnMovementInput();
        }

        private void OnMovementInput()
        {
            _currentMovement = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > _epsilon)
            {
                _currentMovement = _camera.transform.TransformDirection(_inputService.Axis);

                //_currentMovement.y = 0;
                //_currentMovement.Normalize();

                //transform.forward = _currentMovement;
            }

            HandleGravity();

            _characterController.Move(_currentMovement * _speed * Time.deltaTime);
        }

        private void HandleGravity()
        {
            if (_characterController.isGrounded)
            {
                float groundedGravity = -.05f;
                _currentMovement.y = groundedGravity;
            }
            else
            {
                float gravity = -9.8f;
                _currentMovement.y = gravity;
            }
        }
    }
}